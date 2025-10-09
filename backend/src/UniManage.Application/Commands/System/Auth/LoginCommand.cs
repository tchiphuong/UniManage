using Dapper;
using Duende.IdentityModel.Client;
using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Models;
using UniManage.Resource;

namespace UniManage.Api.Domains.Command.System.Auth
{
    public class LoginCommand : CoreBaseCommand, IRequest<CoreResponse>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }

        public class Result
        {
            /// <summary>
            /// Token truy cập (JWT).
            /// </summary>
            public string? AccessToken { get; set; }

            /// <summary>
            /// Token làm mới (refresh).
            /// </summary>
            public string? RefreshToken { get; set; }

            /// <summary>
            /// Thời điểm hết hạn của access token.
            /// </summary>
            public DateTime AccessTokenExpiresAt { get; set; }

            /// <summary>
            /// Tên đăng nhập (định danh chính).
            /// </summary>
            public string? Username { get; set; }

            /// <summary>
            /// Tên hiển thị.
            /// </summary>
            public string? DisplayName { get; set; }

            /// <summary>
            /// Email hoặc tên đăng nhập.
            /// </summary>
            public string? Email { get; set; }

            /// <summary>
            /// Danh sách role của người dùng.
            /// </summary>
            public List<string> Roles { get; set; }
        }
    }

    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, CoreResponse>
    {
        public async Task<CoreResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            CoreResponse response = null;
            CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
            //logData.Parameter = new List<CoreParamModel>();

            try
            {
                using (DbContext dbContext = new DbContext(openTransaction: true))
                {
                    var sql = @"
                        SELECT TOP 1
                            [Id],
                            [UserName],
                            [Password],
                            [EmployeeCode],
                            [RoleCode],
                            [Status]
                        FROM [dbo].[sy_users]
                        WHERE
                            [UserName] = @UserName
                            AND [Password] = @Password
                            AND [Status] = 1
                    ";
                    var user = await dbContext.connection.QueryFirstOrDefaultAsync<dynamic>(
                        sql,
                        new { UserName = request.Username, request.Password },
                        dbContext.transaction
                    );

                    if (user != null)
                    {
                        using (var client = new HttpClient())
                        {
                            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:44370", cancellationToken);
                            if (disco.IsError)
                            {
                                response = new CoreResponse(CoreApiReturnCode.ExceptionOccurred, "IdentityServer discovery failed");
                            }
                            else
                            {
                                var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
                                {
                                    Address = disco.TokenEndpoint,
                                    ClientId = "client",
                                    ClientSecret = "secret",
                                    UserName = "testuser",
                                    Password = "testpassword",
                                    Scope = "api1 offline_access"
                                }, cancellationToken);

                                if (tokenResponse.IsError)
                                {
                                    response = new CoreResponse(CoreApiReturnCode.ExceptionOccurred, tokenResponse.ErrorDescription ?? tokenResponse.Error);
                                }
                                else
                                {
                                    // Sử dụng LoginCommand.Result để trả về dữ liệu chuẩn hóa
                                    var result = new LoginCommand.Result
                                    {
                                        AccessToken = tokenResponse.AccessToken,
                                        RefreshToken = tokenResponse.RefreshToken,
                                        AccessTokenExpiresAt = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn),
                                        Username = user.UserName,
                                        DisplayName = user.UserName,
                                        Email = null,
                                        Roles = new List<string> { user.RoleCode?.ToString() },
                                    };

                                    response = new CoreResponse(CoreApiReturnCode.Succeed, "", result);
                                }
                            }
                        }
                    }
                    else
                    {
                        response = new CoreResponse(CoreApiReturnCode.ExceptionOccurred, "Invalid username or password");
                    }
                    await dbContext.transaction.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                response = new CoreResponse(CoreApiReturnCode.ExceptionOccurred, CoreResource.Common_msg_ExceptionOccurred);
                #region write log
                logData.Result = response.Data;
                logData.Message = ex.ToString();
                logData.IsException = 1;
                logData.ReturnCode = response.ReturnCode;
                #endregion
            }

            UniLogManager.WriteApiLog(logData);

            return await Task.FromResult(response);
        }
    }
}