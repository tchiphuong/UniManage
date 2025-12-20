using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Application.Commands.System.User
{
	public sealed class CreateUserCommand : BaseCommand, IRequest<ApiResponse<CreateUserCommand.Response>>
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string EmployeeCode { get; set; } = default!;
        public string RoleCode { get; set; } = default!;
        public string? Email { get; set; }
        public string Status { get; set; } = "ACTIVE";

        public class Response
        {
            public int Id { get; set; }
            public string Username { get; set; } = default!;
        }
    }

	public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
	{
		public CreateUserCommandValidator()
		{
			RuleFor(x => x.Username)
				.NotEmpty().WithMessage("Username is required")
				.Must(ValidationHelper.IsValidUserCode).WithMessage("Username must be 3-50 alphanumeric characters");

			RuleFor(x => x.Password)
				.NotEmpty().WithMessage("Password is required")
				.Must(PasswordHelper.IsValidPassword).WithMessage("Password must be at least 6 characters");

			RuleFor(x => x.EmployeeCode)
				.NotEmpty().WithMessage("Employee code is required")
				.Must(ValidationHelper.IsValidEmployeeCode).WithMessage("Employee code must be 3-20 alphanumeric characters");

			RuleFor(x => x.RoleCode)
				.NotEmpty().WithMessage("Role code is required")
				.MaximumLength(20).WithMessage("Role code must not exceed 20 characters");

			When(x => !string.IsNullOrEmpty(x.Email), () =>
			{
				RuleFor(x => x.Email)
					.Must(email => ValidationHelper.IsValidEmail(email!)).WithMessage("Invalid email format")
					.MaximumLength(100).WithMessage("Email must not exceed 100 characters");
			});
		}
	}

	/// <summary>
	/// Handler for CreateUserCommand using utility functions for validation, password hashing, and database operations
	/// </summary>
	public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<CreateUserCommand.Response>>
	{
		public async Task<ApiResponse<CreateUserCommand.Response>> Handle(
			CreateUserCommand request,
			CancellationToken ct)
		{
			try
			{
				// Hash password using PasswordHelper
				var hashedPassword = PasswordHelper.HashPassword(request.Password);

				// Generate Uuid
				var uuid = Guid.NewGuid();

				// Insert new user using DbContext with transaction
				var insertSql = @"
					INSERT INTO [dbo].[sy_users] 
					([Uuid], [UserName], [Password], [EmployeeCode], [RoleCode], [Email], [Status], [CreatedBy], [CreatedAt])
					VALUES 
					(@Uuid, @Username, @Password, @EmployeeCode, @RoleCode, @Email, @Status, @CreatedBy, SYSUTCDATETIME());
					SELECT SCOPE_IDENTITY();";

				using var dbContext = new DbContext(openTransaction: true);
				try
				{
					var userId = await dbContext.connection.ExecuteScalarAsync<int>(insertSql, new
					{
						Uuid = uuid,
						request.Username,
						Password = hashedPassword,
						request.EmployeeCode,
						request.RoleCode,
						request.Email,
						request.Status,
						CreatedBy = ApplicationConstants.Defaults.SystemUser
					}, dbContext.transaction);

					await dbContext.CommitAsync();

					UniLogger.Info($"User created successfully: {request.Username} (ID: {userId})");

					return ResponseHelper.Success(
						new CreateUserCommand.Response
						{
							Id = userId,
							Username = request.Username
						},
						"User created successfully");
				}
				catch
				{
					await dbContext.RollbackAsync();
					throw;
				}
			}
			catch (Exception ex)
			{
				UniLogger.Error($"Error creating user {request.Username}: {ex.Message}", ex);
				return ResponseHelper.Error<CreateUserCommand.Response>("Failed to create user");
			}
		}
	}
}
