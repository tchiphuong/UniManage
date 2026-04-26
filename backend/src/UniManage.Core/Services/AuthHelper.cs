using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using UniManage.Core.Constant;
using DbContext = UniManage.Core.Database.DbContext;
using UniManage.Core.Utilities;
using UniManage.Model.Entities;
using UniManage.Model.Common;
using UniManage.Resource;
using Dapper;

namespace UniManage.Core.Services
{
    /// <summary>
    /// Helper cung cấp các hàm xử lý xác thực và quản lý trạng thái tài khoản (Static để tối ưu performance).
    /// </summary>
    public static class AuthHelper
    {
        /// <summary>
        /// Kiểm tra trạng thái tài khoản (Active/Locked).
        /// </summary>
        /// <param name="username">Tên đăng nhập.</param>
        /// <param name="log">Đối tượng log từ API truyền vào.</param>
        /// <param name="ct">Token hủy bỏ tác vụ.</param>
        /// <returns>Kết quả kiểm tra kèm thông tin bảo mật của người dùng.</returns>
        public static async Task<(bool Success, string? Error, UserSecurityDto? User)> ValidateUserStatusAsync(string username, CoreLogModel log, CancellationToken ct = default)
        {
            log.Parameter.Add(new CoreParamModel(nameof(username), username));
            using var dbContext = new DbContext();
            
            // Sử dụng EF Core LINQ để tìm người dùng (Type-safe)
            var userEntity = await dbContext.Set<sy_users>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Username == username, ct);

            if (userEntity == null)
            {
                return (false, CoreResource.auth_userNotFound, null);
            }

            var user = new UserSecurityDto
            {
                Id = userEntity.Id,
                Status = userEntity.Status ?? string.Empty,
                LockedUntil = userEntity.LockedUntil,
                FailedLoginCount = userEntity.FailedLoginCount
            };

            if (user.Status != CoreCommon.Value.Commonstatus.Active)
            {
                return (false, CoreResource.auth_accountInactive, user);
            }

            if (user.LockedUntil.HasValue && user.LockedUntil.Value > DateTimeHelper.Now)
            {
                return (false, CoreResource.auth_accountLocked, user);
            }

            return (true, null, user);
        }

        /// <summary>
        /// Xử lý khi đăng nhập thành công (Reset số lần sai, mở khóa).
        /// </summary>
        /// <param name="username">Tên đăng nhập.</param>
        /// <param name="log">Đối tượng log từ API truyền vào.</param>
        /// <param name="ct">Token hủy bỏ tác vụ.</param>
        public static async Task HandleLoginSuccessAsync(string username, CoreLogModel log, CancellationToken ct = default)
        {
            log.Parameter.Add(new CoreParamModel(nameof(username), username));
            using var dbContext = new DbContext(openTransaction: true);
            
            // Sử dụng EF Core để cập nhật (Dễ bảo trì)
            var user = await dbContext.Set<sy_users>()
                .FirstOrDefaultAsync(u => u.Username == username, ct);

            if (user != null)
            {
                user.FailedLoginCount = 0;
                user.LockedUntil = null;
                user.UpdatedAt = DateTimeHelper.Now;
                user.UpdatedBy = username;

                await dbContext.SaveChangesAsync(ct);
                await dbContext.CommitAsync(ct);
            }
        }

        /// <summary>
        /// Xử lý khi đăng nhập thất bại (Tăng số lần sai, khóa tài khoản nếu cần).
        /// </summary>
        /// <param name="username">Tên đăng nhập.</param>
        /// <param name="log">Đối tượng log từ API truyền vào.</param>
        /// <param name="ct">Token hủy bỏ tác vụ.</param>
        public static async Task HandleLoginFailureAsync(string username, CoreLogModel log, CancellationToken ct = default)
        {
            log.Parameter.Add(new CoreParamModel(nameof(username), username));
            using var dbContext = new DbContext(openTransaction: true);
            
            // Sử dụng EF Core cho logic nghiệp vụ tăng số lần sai
            var user = await dbContext.Set<sy_users>()
                .FirstOrDefaultAsync(u => u.Username == username, ct);

            if (user != null)
            {
                user.FailedLoginCount = (user.FailedLoginCount ?? 0) + 1;
                
                if (user.FailedLoginCount >= 5)
                {
                    user.LockedUntil = DateTimeHelper.Now.AddMinutes(30);
                }

                user.UpdatedAt = DateTimeHelper.Now;
                user.UpdatedBy = username;

                await dbContext.SaveChangesAsync(ct);
                await dbContext.CommitAsync(ct);
            }
        }

        /// <summary>
        /// Cập nhật hoặc đăng ký mới thiết bị và FCM Token cho người dùng.
        /// </summary>
        /// <param name="userId">ID người dùng.</param>
        /// <param name="deviceId">ID thiết bị.</param>
        /// <param name="fcmToken">Token từ Firebase.</param>
        /// <param name="deviceType">Loại thiết bị (iOS, Android, Web).</param>
        /// <param name="log">Đối tượng log từ API truyền vào.</param>
        /// <param name="ct">Token hủy bỏ tác vụ.</param>
        public static async Task UpdateDeviceTokenAsync(long userId, string? deviceId, string? fcmToken, string? deviceType, CoreLogModel log, CancellationToken ct = default)
        {
            log.Parameter.Add(new CoreParamModel(nameof(userId), userId));
            log.Parameter.Add(new CoreParamModel(nameof(deviceId), deviceId));
            
            if (string.IsNullOrEmpty(deviceId)) return;

            using var dbContext = new DbContext(openTransaction: true);
            var sql = @"
                IF EXISTS (SELECT 1 FROM [dbo].[sy_user_devices] WHERE [UserId] = @UserId AND [DeviceId] = @DeviceId)
                BEGIN
                    UPDATE [dbo].[sy_user_devices]
                    SET [FcmToken] = @FcmToken,
                        [DeviceType] = @DeviceType,
                        [LastActiveAt] = GETDATE()
                    WHERE [UserId] = @UserId AND [DeviceId] = @DeviceId
                END
                ELSE
                BEGIN
                    INSERT INTO [dbo].[sy_user_devices] ([UserId], [DeviceId], [FcmToken], [DeviceType], [CreatedAt], [LastActiveAt])
                    VALUES (@UserId, @DeviceId, @FcmToken, @DeviceType, GETDATE(), GETDATE())
                END";

            await dbContext.Database.GetDbConnection().ExecuteAsync(sql, new { UserId = userId, DeviceId = deviceId, FcmToken = fcmToken, DeviceType = deviceType }, dbContext.transaction);
            await dbContext.CommitAsync(ct);
        }

        /// <summary>
        /// Đăng ký Public Key sinh trắc học cho thiết bị.
        /// </summary>
        public static async Task RegisterBiometricKeyAsync(long userId, string deviceId, string publicKey, string? deviceName, CoreLogModel log, CancellationToken ct = default)
        {
            log.Parameter.Add(new CoreParamModel(nameof(userId), userId));
            log.Parameter.Add(new CoreParamModel(nameof(deviceId), deviceId));

            using var dbContext = new DbContext(openTransaction: true);
            var sql = @"
                IF EXISTS (SELECT 1 FROM [dbo].[sy_user_biometrics] WHERE [UserId] = @UserId AND [DeviceId] = @DeviceId)
                BEGIN
                    UPDATE [dbo].[sy_user_biometrics]
                    SET [PublicKey] = @PublicKey,
                        [DeviceName] = @DeviceName
                    WHERE [UserId] = @UserId AND [DeviceId] = @DeviceId
                END
                ELSE
                BEGIN
                    INSERT INTO [dbo].[sy_user_biometrics] ([UserId], [DeviceId], [PublicKey], [DeviceName], [CreatedAt])
                    VALUES (@UserId, @DeviceId, @PublicKey, @DeviceName, GETDATE())
                END";

            await dbContext.Database.GetDbConnection().ExecuteAsync(sql, new { UserId = userId, DeviceId = deviceId, PublicKey = publicKey, DeviceName = deviceName }, dbContext.transaction);
            await dbContext.CommitAsync(ct);
        }

        /// <summary>
        /// Xác thực chữ ký sinh trắc học từ thiết bị.
        /// </summary>
        public static async Task<bool> VerifyBiometricSignatureAsync(long userId, string deviceId, string challenge, string signature, CoreLogModel log, CancellationToken ct = default)
        {
            log.Parameter.Add(new CoreParamModel(nameof(userId), userId));
            log.Parameter.Add(new CoreParamModel(nameof(deviceId), deviceId));

            using var dbContext = new DbContext();
            
            // Sử dụng EF Core LINQ để lấy Public Key
            var publicKey = await dbContext.Set<sy_user_biometrics>()
                .AsNoTracking()
                .Where(b => b.UserId == userId && b.DeviceId == deviceId)
                .Select(b => b.PublicKey)
                .FirstOrDefaultAsync(ct);

            if (string.IsNullOrEmpty(publicKey)) return false;

            try
            {
                using var rsa = RSA.Create();
                
                // Giả định PublicKey được lưu dưới dạng Base64 của SubjectPublicKeyInfo (SPKI)
                byte[] publicKeyBytes = Convert.FromBase64String(publicKey);
                rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

                byte[] challengeBytes = Encoding.UTF8.GetBytes(challenge);
                byte[] signatureBytes = Convert.FromBase64String(signature);

                // Xác thực chữ ký dùng SHA256 và PKCS1 Padding
                return rsa.VerifyData(challengeBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = "Biometric verification failed: " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Tìm hoặc tạo mới người dùng từ thông tin Social Profile.
        /// </summary>
        public static async Task<sy_users?> GetOrCreateSocialUserAsync(UniManage.Core.Services.Social.SocialUserProfile profile, string internalSecret, CoreLogModel log, CancellationToken ct = default)
        {
            using var dbContext = new DbContext(openTransaction: true);
            
            // 1. Tìm theo Email hoặc Provider Id
            var user = await dbContext.Set<sy_users>()
                .FirstOrDefaultAsync(u => u.Email == profile.Email || u.SocialProviderId == profile.ProviderUserId, ct);

            if (user != null)
            {
                // Cập nhật thông tin nếu cần
                user.SocialProvider = profile.Provider;
                user.SocialProviderId = profile.ProviderUserId;
                user.UpdatedAt = DateTimeHelper.Now;
                user.UpdatedBy = "SYSTEM_SOCIAL";
                
                // Đồng bộ mật khẩu nội bộ để đảm bảo login được
                user.Password = PasswordHelper.HashPassword(internalSecret);
                
                await dbContext.SaveChangesAsync(ct);
                await dbContext.CommitAsync(ct);
                return user;
            }

            // 2. Tạo mới nếu chưa có
            user = new sy_users
            {
                Username = profile.Email.Split('@')[0] + "_" + Guid.NewGuid().ToString("N").Substring(0, 4),
                Email = profile.Email,
                Status = CoreCommon.Value.Commonstatus.Active,
                Password = PasswordHelper.HashPassword(internalSecret), // Đặt mật khẩu bí mật
                SocialProvider = profile.Provider,
                SocialProviderId = profile.ProviderUserId,
                CreatedAt = DateTimeHelper.Now,
                CreatedBy = "SYSTEM_SOCIAL"
            };

            dbContext.Set<sy_users>().Add(user);
            await dbContext.SaveChangesAsync(ct);
            await dbContext.CommitAsync(ct);

            return user;
        }
    }

    /// <summary>
    /// DTO chứa thông tin bảo mật của người dùng.
    /// </summary>
    public class UserSecurityDto
    {
        public long Id { get; set; }
        public string Status { get; set; } = default!;
        public DateTime? LockedUntil { get; set; }
        public int? FailedLoginCount { get; set; }
    }
}
