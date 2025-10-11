using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Models;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Resource;

namespace UniManage.Application.Commands.System.User
{
	public sealed class CreateUserCommand : IRequest<ApiResponse<CreateUserCommand.Response>>
	{
		public string Username { get; init; } = default!;
		public string Password { get; init; } = default!;
		public string EmployeeCode { get; init; } = default!;
		public string RoleCode { get; init; } = default!;
		public string? Email { get; init; }
		public int Status { get; init; } = 1;

		public sealed class Response
		{
			public int Id { get; init; }
			public string Username { get; init; } = default!;
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
				// Check if username already exists using DatabaseHelper
				if (await DatabaseHelper.UserCodeExistsAsync(request.Username))
				{
					UniLogger.Warn($"Attempt to create user with existing username: {request.Username}");
					return ResponseHelper.Error<CreateUserCommand.Response>("Username already exists");
				}

				// Check if employee code already exists
				if (await DatabaseHelper.EmployeeCodeExistsAsync(request.EmployeeCode))
				{
					UniLogger.Warn($"Attempt to create user with existing employee code: {request.EmployeeCode}");
					return ResponseHelper.Error<CreateUserCommand.Response>("Employee code already exists");
				}

				// Check if email already exists (if provided)
				if (!string.IsNullOrWhiteSpace(request.Email) && await DatabaseHelper.EmailExistsAsync(request.Email))
				{
					UniLogger.Warn($"Attempt to create user with existing email: {request.Email}");
					return ResponseHelper.Error<CreateUserCommand.Response>("Email already exists");
				}

				// Hash password using PasswordHelper
				var hashedPassword = PasswordHelper.HashPassword(request.Password);

				// Insert new user using DatabaseHelper transaction wrapper
				var insertSql = @"
					INSERT INTO [dbo].[sy_users] 
					([UserName], [Password], [EmployeeCode], [RoleCode], [Email], [Status], [CreatedBy], [CreatedAt])
					VALUES 
					(@Username, @Password, @EmployeeCode, @RoleCode, @Email, @Status, 'SYSTEM', SYSUTCDATETIME());
					SELECT SCOPE_IDENTITY();";

				var userId = await DatabaseHelper.ExecuteWithTransactionAsync<int>(insertSql, new
				{
					request.Username,
					Password = hashedPassword,
					request.EmployeeCode,
					request.RoleCode,
					request.Email,
					request.Status
				});

				UniLogger.Info($"User created successfully: {request.Username} (ID: {userId})");

				return ResponseHelper.Success(
					new CreateUserCommand.Response
					{
						Id = userId,
						Username = request.Username
					},
					"User created successfully");
			}
			catch (Exception ex)
			{
				UniLogger.Error($"Error creating user {request.Username}: {ex.Message}", ex);
				return ResponseHelper.Error<CreateUserCommand.Response>("Failed to create user");
			}
		}
	}
}
