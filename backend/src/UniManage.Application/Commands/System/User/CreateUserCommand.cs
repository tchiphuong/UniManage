using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.User;

#region Command

/// <summary>
/// Command to create a new user
/// </summary>
public sealed class CreateUserCommand : BaseCommand, IRequest<ApiResponse<CreateUserCommand.Response>>
{
    public string Username { get; init; } = default!;
    public string DisplayName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string? PhoneNumber { get; init; }
    public string? EmployeeCode { get; init; }
    public string Password { get; init; } = default!;
    public string Status { get; init; } = default!;
    public List<string> RoleCode { get; init; } = default!;

    // Internal - set by controller from token
    internal string CreatedBy { get; init; } = default!;

    public sealed class Response
    {
        public bool Success => Id > 0;
        public int Id { get; init; } = default!;
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
            .Cascade(CascadeMode.Stop) // D?ng ngay n?u l?i d?u tiên xu?t hi?n
            .NotEmpty().WithMessage("Username is required")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters")
            .Must(ValidationHelper.IsValidUserCode).WithMessage(CoreResource.Validation_msg_AlphanumericOnly)
            .MustAsync(async (username, cancel) => !await IsUsernameExistsAsync(username))
            .WithMessage(CoreResource.Validation_msg_UsernameAlreadyTaken);

        // 2. Validate Display Name
        RuleFor(x => x.DisplayName)
            .NotEmpty().WithMessage("Display name is required")
            .MaximumLength(200).WithMessage("Display name must not exceed 200 characters");
        // Luu ý: Không ch?n ký t? d?c bi?t quá m?c d? h? tr? tên ti?ng Vi?t

        // 3. Validate Email
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Email is required")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters")
            .EmailAddress().WithMessage(CoreResource.Validation_msg_InvalidEmail)
            .MustAsync(async (email, cancel) => !await IsEmailExistsAsync(email))
            .WithMessage(CoreResource.Validation_msg_EmailAlreadyRegistered);

        // 4. Validate Phone
        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20)
            .Must(ValidationHelper.IsValidPhoneNumber)
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber))
            .WithMessage(CoreResource.Validation_msg_InvalidPhone);

        // 5. Validate Password (Nâng cao)
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .Matches(@"[A-Z]").WithMessage(CoreResource.Validation_msg_MustContainUppercase)
            .Matches(@"[a-z]").WithMessage(CoreResource.Validation_msg_MustContainLowercase)
            .Matches(@"[0-9]").WithMessage(CoreResource.Validation_msg_MustContainNumber)
            .Matches(@"[\!\?\*\.]").WithMessage("Password must contain at least one special character (!?*.)");

        // 6. Validate Status
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required")
            .Must(status => status == "Active" || status == "Inactive")
            .WithMessage("Status must be either 'Active' or 'Inactive'");

        // 7. Validate RoleCode
        RuleFor(x => x.RoleCode)
            .NotEmpty().WithMessage("At least one role must be assigned");
    }

    public static Task<bool> IsUsernameExistsAsync(string username)
    {
        using (var dbContext = new DbContext())
        {
            return dbContext.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_users WHERE UserName = @Username) THEN 1 ELSE 0 END",
                new
                {
                    Username = username
                });
        }
    }

    public static Task<bool> IsEmailExistsAsync(string email)
    {
        using (var dbContext = new DbContext())
        {
            return dbContext.ExecuteScalarAsync<bool>(
                "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_users WHERE Email = @Email) THEN 1 ELSE 0 END",
                new
                {
                    Email = email
                });
        }
    }
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
                new CoreParamModel(nameof(request.DisplayName), request.DisplayName),
                new CoreParamModel(nameof(request.Email), request.Email),
                new CoreParamModel(nameof(request.PhoneNumber), request.PhoneNumber),
                new CoreParamModel(nameof(request.EmployeeCode), request.EmployeeCode),
                new CoreParamModel(nameof(request.Status), request.Status),
                new CoreParamModel(nameof(request.RoleCode), request.RoleCode),
            }
        };

        // Create user and assign role in transaction
        using (var dbContext = new DbContext(openTransaction: true))
        {
            try
            {
                // Hash password
                var passwordHash = PasswordHelper.HashPassword(request.Password);

                // Insert user
                var id = await dbContext.ExecuteScalarAsync<int>(@"INSERT INTO sy_users (
                                                    Username, 
                                                    DisplayName, 
                                                    Email, 
                                                    PhoneNumber, 
                                                    PasswordHash, 
                                                    EmployeeCode, 
                                                    Status, 
                                                    CreatedBy, 
                                                    CreatedAt
                                                )
                                                VALUES (
                                                    @Username, 
                                                    @DisplayName, 
                                                    @Email, 
                                                    @PhoneNumber, 
                                                    @PasswordHash, 
                                                    @EmployeeCode, 
                                                    @Status, 
                                                    @CreatedBy, 
                                                    GETDATE()
                                                );
                                                SELECT SCOPE_IDENTITY();
                                                ",
                new
                {
                    request.Username,
                    request.DisplayName,
                    request.Email,
                    request.PhoneNumber,
                    PasswordHash = passwordHash,
                    request.EmployeeCode,
                    request.Status,
                    CreatedBy = request.HeaderInfo!.Username
                },
                ct);

                // Assign role
                await dbContext.ExecuteAsync(@"INSERT INTO sy_user_roles (
                                                    Username, 
                                                    RoleCode, 
                                                    CreatedBy, 
                                                    CreatedAt
                                                )
                                                VALUES (
                                                    @Username, 
                                                    @RoleCode, 
                                                    @Username, 
                                                    GETDATE()
                                                )",
                new
                {
                    request.Username,
                    request.RoleCode
                },
                ct);

                await dbContext.transaction.CommitAsync(ct);

                var response = ResponseHelper.Success(new CreateUserCommand.Response
                {
                    Id = id,
                }, CoreResource.User_msg_CreateSuccess);

                log.Result = response;
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                await dbContext.transaction.RollbackAsync(ct);
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);
                return ResponseHelper.Error<CreateUserCommand.Response>(CoreResource.Common_msg_ExceptionOccurred);
            }
        }
    }
}

#endregion