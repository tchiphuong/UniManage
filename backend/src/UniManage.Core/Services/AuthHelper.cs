using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Core.Services
{
    /// <summary>
    /// Helper providing authentication and account status management functions (Static for performance).
    /// </summary>
    public static class AuthHelper
    {
        /// <summary>
        /// Validates user account status (Active/Locked).
        /// </summary>
        /// <param name="username">Login username.</param>
        /// <param name="log">Log object from the calling API.</param>
        /// <param name="ct">Cancellation token.</param>
        /// <returns>Validation result along with user's security information.</returns>
        public static async Task<(bool Success, string? Error, UserSecurityDto? User)> ValidateUserStatusAsync(string username, CoreLogModel log, CancellationToken ct = default)
        {
            log.Parameter.Add(new CoreParamModel(nameof(username), username));
            using var dbContext = new DbContext();
            
            // Use EF Core LINQ to find user (Type-safe)
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
        /// Processes successful login (Resets failure count, unlocks account).
        /// </summary>
        /// <param name="username">Login username.</param>
        /// <param name="log">Log object from the calling API.</param>
        /// <param name="ct">Cancellation token.</param>
        public static async Task HandleLoginSuccessAsync(string username, CoreLogModel log, CancellationToken ct = default)
        {
            log.Parameter.Add(new CoreParamModel(nameof(username), username));
            using var dbContext = new DbContext(openTransaction: true);
            
            // Use EF Core to update (Maintainable)
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
        /// Processes failed login (Increments failure count, locks account if needed).
        /// </summary>
        /// <param name="username">Login username.</param>
        /// <param name="log">Log object from the calling API.</param>
        /// <param name="ct">Cancellation token.</param>
        public static async Task HandleLoginFailureAsync(string username, CoreLogModel log, CancellationToken ct = default)
        {
            log.Parameter.Add(new CoreParamModel(nameof(username), username));
            using var dbContext = new DbContext(openTransaction: true);
            
            // Use EF Core for increment business logic
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
        /// Updates or registers a new device and FCM Token for a user.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <param name="deviceId">Device unique ID.</param>
        /// <param name="fcmToken">Firebase FCM Token.</param>
        /// <param name="deviceType">Device type (iOS, Android, Web).</param>
        /// <param name="log">Log object from the calling API.</param>
        /// <param name="ct">Cancellation token.</param>
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
        /// Registers a biometric Public Key for a specific device.
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
        /// Verifies a biometric signature from a device.
        /// </summary>
        public static async Task<bool> VerifyBiometricSignatureAsync(long userId, string deviceId, string challenge, string signature, CoreLogModel log, CancellationToken ct = default)
        {
            log.Parameter.Add(new CoreParamModel(nameof(userId), userId));
            log.Parameter.Add(new CoreParamModel(nameof(deviceId), deviceId));

            using var dbContext = new DbContext();
            
            // Use EF Core LINQ to fetch Public Key
            var publicKey = await dbContext.Set<sy_user_biometrics>()
                .AsNoTracking()
                .Where(b => b.UserId == userId && b.DeviceId == deviceId)
                .Select(b => b.PublicKey)
                .FirstOrDefaultAsync(ct);

            if (string.IsNullOrEmpty(publicKey)) return false;

            try
            {
                using var rsa = RSA.Create();
                
                // Assume PublicKey is stored as Base64 of SubjectPublicKeyInfo (SPKI)
                byte[] publicKeyBytes = Convert.FromBase64String(publicKey);
                rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

                byte[] challengeBytes = Encoding.UTF8.GetBytes(challenge);
                byte[] signatureBytes = Convert.FromBase64String(signature);

                // Verify signature using SHA256 and PKCS1 Padding
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
        /// Retrieves or creates a new user from Social Profile information.
        /// </summary>
        public static async Task<sy_users?> GetOrCreateSocialUserAsync(UniManage.Core.Services.Social.SocialUserProfile profile, string internalSecret, CoreLogModel log, CancellationToken ct = default)
        {
            using var dbContext = new DbContext(openTransaction: true);
            
            // 1. Search by Email or Provider Id
            var user = await dbContext.Set<sy_users>()
                .FirstOrDefaultAsync(u => u.Email == profile.Email || u.SocialProviderId == profile.ProviderUserId, ct);

            if (user != null)
            {
                // Update profile info if needed
                user.SocialProvider = profile.Provider;
                user.SocialProviderId = profile.ProviderUserId;
                user.UpdatedAt = DateTimeHelper.Now;
                user.UpdatedBy = "SYSTEM_SOCIAL";
                
                // Sync internal secret hash to ensure standard login works
                user.Password = PasswordHelper.HashPassword(internalSecret);
                
                await dbContext.SaveChangesAsync(ct);
                await dbContext.CommitAsync(ct);
                return user;
            }

            // 2. Create new user if not found
            user = new sy_users
            {
                Username = profile.Email.Split('@')[0] + "_" + Guid.NewGuid().ToString("N").Substring(0, 4),
                Email = profile.Email,
                Status = CoreCommon.Value.Commonstatus.Active,
                Password = PasswordHelper.HashPassword(internalSecret), // Set a secret password
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
    /// DTO containing user's security status information.
    /// </summary>
    public class UserSecurityDto
    {
        public long Id { get; set; }
        public string Status { get; set; } = default!;
        public DateTime? LockedUntil { get; set; }
        public int? FailedLoginCount { get; set; }
    }
}
