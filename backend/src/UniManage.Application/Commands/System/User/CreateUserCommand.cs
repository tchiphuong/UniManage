using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
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
        // 1. Validate Username
        RuleFor(x => x.Username)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Username is required")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters")
            .Must(ValidationHelper.IsValidUserCode).WithMessage(CoreResource.validation_alphanumericOnly)
            .MustAsync(async (username, cancel) => !await IsUsernameExistsAsync(username))
            .WithMessage(CoreResource.validation_usernameAlreadyTaken);

        // 2. Validate Email - Removed
        /*
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters")
            .EmailAddress().WithMessage(CoreResource.validation_invalidEmail)
            .MustAsync(async (email, cancel) => !await IsEmailExistsAsync(email))
            .WithMessage(CoreResource.validation_emailAlreadyRegistered);
        */

        // 3. Validate Password
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .Matches(@"[A-Z]").WithMessage(CoreResource.validation_mustContainUppercase)
            .Matches(@"[a-z]").WithMessage(CoreResource.validation_mustContainLowercase)
            .Matches(@"[0-9]").WithMessage(CoreResource.validation_mustContainNumber)
            .Matches(@"[\!\?\*\.]").WithMessage("Password must contain at least one special character (!?*.)");

        // 4. Validate Status
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .Must(status => status == "Active" || status == "Inactive")
            .WithMessage("Status must be either 'Active' or 'Inactive'");

        // 5. Validate RoleCode
        RuleFor(x => x.RoleCode)
            .NotEmpty().WithMessage("At least one role must be assigned");
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
