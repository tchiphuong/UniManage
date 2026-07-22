using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyUser.Commands;

#region Command

/// <summary>
/// Command to change user password
/// </summary>
public sealed class ChangePasswordCommand : BaseCommand, IRequest<ApiResponse<bool>>
{
    [Newtonsoft.Json.JsonIgnore]
    public string Username { get; set; } = default!;
    public string OldPassword { get; init; } = default!;
    public string NewPassword { get; init; } = default!;
    public string ConfirmPassword { get; init; } = default!;
}

#endregion

#region Handler

/// <summary>
/// Handler for ChangePasswordCommand
/// </summary>
public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var log = new ApiLogModel(request.HeaderInfo)
        {
            Parameter = new List<LogParamModel>
            {
                new(nameof(request.Username), request.Username)
            }
        };

        try
        {
            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var user = await dbContext.Set<SyUsers>()
                        .FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);

                    if (user == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<bool>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                        log.Message = notFoundResponse.Message;
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        return notFoundResponse;
                    }

                    // Verify old password
                    if (!PasswordHelper.VerifyPassword(request.OldPassword, user.Password))
                    {
                        var errorResponse = ResponseHelper.Error<bool>(CoreResource.auth_oldPasswordIncorrect);
                        log.Message = errorResponse.Message;
                        log.ReturnCode = errorResponse.ReturnCode;
                        return errorResponse;
                    }

                    // Update new password
                    user.Password = PasswordHelper.HashPassword(request.NewPassword);
                    user.UpdatedBy = request.Username;
                    user.UpdatedAt = DateTimeHelper.Now;

                    await dbContext.SaveChangesAsync(cancellationToken);
                    await dbContext.CommitAsync();

                    var response = ResponseHelper.Success(true, CoreResource.auth_passwordChanged);
                    
                    log.Result = true;
                    log.Message = response.Message;
                    log.ReturnCode = response.ReturnCode;

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync();
                    log.IsException = true;
                    log.Message = ex.Message;
                    log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                    return ResponseHelper.Error<bool>(CoreResource.common_error);
                }
            }
        }
        finally
        {
            UniLogManager.WriteApiLog(log);
        }
    }
}

#endregion






