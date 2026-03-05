using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using UniManage.Application.Utilities;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Commands.System.User;

#region Command

/// <summary>
/// Command to create a new user
/// </summary>
public sealed class CreateUserCommand : BaseCommand, IRequest<ApiResponse<CreateUserCommand.Response>>
{
    public string Username { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string? EmployeeCode { get; init; }
    public string Password { get; init; } = default!;
    public string Status { get; init; } = default!;
    public List<string> RoleCode { get; init; } = default!;

    // Note: DisplayName and PhoneNumber should be stored in hr_employees table if needed
    // They don't exist in sy_users table schema

    public sealed class Response
    {
        public bool Success => Id > 0;
        public long Id { get; init; } = default!;
    }
}

#endregion

#region Validator

/// <summary>
/// Validator for CreateUserCommand
/// </summary>
public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    // Inject Repository ho?c Service d? check DB
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_username))
            .DependentRules(() =>
            {
                RuleFor(x => x.Username)
                    .Length(3, 50)
                    .WithMessage(string.Format(CoreResource.validation_length, CoreResource.lbl_username, 3, 50))
                    .Must(ValidationHelper.IsValidUserCode)
                    .WithMessage(CoreResource.validation_alphanumericOnly)
                    .MustAsync(async (username, cancel) => !await IsUsernameExistsAsync(username))
                    .WithMessage(CoreResource.validation_usernameAlreadyTaken);
            });

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_password))
            .DependentRules(() =>
            {
                RuleFor(x => x.Password)
                    .Password(CoreResource.lbl_password);
            });

        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_status))
            .DependentRules(() =>
            {
                RuleFor(x => x.Status)
                    .Must(status => CoreCommon.Value.Commonstatus.All.Contains(status))
                    .WithMessage(CoreResource.validation_invalidStatus);
            });

        RuleFor(x => x.RoleCode)
            .NotEmpty()
            .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_role));
    }

    private static async Task<bool> IsUsernameExistsAsync(string username)
    {
        using (var dbContext = new DbContext())
        {
            // Use EF Core LINQ instead of raw SQL
            return await dbContext.Set<sy_users>().AnyAsync(u => u.Username == username);
        }
    }

    // private static async Task<bool> IsEmailExistsAsync(string email) ... Removed
}

#endregion

#region Handler

/// <summary>
/// Handler for CreateUserCommand
/// </summary>
public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<CreateUserCommand.Response>>
{
    public async Task<ApiResponse<CreateUserCommand.Response>> Handle(CreateUserCommand request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Username), request.Username),
                // new CoreParamModel(nameof(request.Email), request.Email),
                new CoreParamModel(nameof(request.EmployeeCode), request.EmployeeCode),
                new CoreParamModel(nameof(request.Status), request.Status),
                new CoreParamModel(nameof(request.RoleCode), request.RoleCode),
            }
        };

        // Create user with EF Core in transaction
        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                // Hash password using PasswordHelper
                var passwordHash = PasswordHelper.HashPassword(request.Password);

                // Create new user entity
                var newUser = new sy_users
                {
                    Username = request.Username,
                    Password = passwordHash, // Entity column is "Password" not "PasswordHash"
                    // Email = request.Email, // Removed
                    EmployeeCode = request.EmployeeCode,
                    RoleCode = request.RoleCode?.FirstOrDefault(),
                    Status = request.Status,
                    CreatedBy = request.HeaderInfo?.Username,
                    CreatedAt = DateTime.Now,
                    RowVersion = new byte[8] // Initialize with empty byte array, SQL Server will manage via rowversion
                };

                // Add to DbContext
                dbContext.Set<sy_users>().Add(newUser);

                // Save changes - EF Core will auto-populate Id
                await dbContext.SaveChangesAsync(ct);

                // Commit transaction
                await dbContext.CommitAsync(ct);

                var response = ResponseHelper.Success(new CreateUserCommand.Response { Id = newUser.Id }, string.Format(CoreResource.crud_createSuccess, CoreResource.entity_user));

                log.Result = response;
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                await dbContext.RollbackAsync(ct);
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);
                return ResponseHelper.Error<CreateUserCommand.Response>(CoreResource.common_exceptionOccurred);
            }
        }
    }
}

#endregion
