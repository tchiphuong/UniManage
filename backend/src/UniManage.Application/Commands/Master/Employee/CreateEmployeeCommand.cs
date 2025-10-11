using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Models;
// using UniManage.Core.Models.Entities; // TODO: Uncomment when T4 generates entities
using UniManage.Resource;
using UniManage.Core.Utilities;

namespace UniManage.Api.Domains.Command.Master.Employee
{
	#region Command
	public class CreateEmployeeCommand : BaseCommand, IRequest<ApiResponse<object>>
	{
		public string? Code { get; set; }
		public string? FullNameVi { get; set; }
		public string? FullNameEn { get; set; }
		public string? FirstNameVi { get; set; }
		public string? FirstNameEn { get; set; }
		public string? LastNameVi { get; set; }
		public string? LastNameEn { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? GenderCode { get; set; }
		public string? Images { get; set; }
		public string? StatusCode { get; set; }
		public string? TypeCode { get; set; }

	}
	#endregion

	#region Validator
	public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
	{
		public CreateEmployeeCommandValidator()
		{
			RuleFor(x => x.Code)
				.NotEmpty()
				.WithMessage(CoreResource.Validation_msg_Required)
				.MaximumLength(50)
				.WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 50))
				.Must(x => !IsExistsCode(x))
				.WithMessage(CoreResource.Validation_msg_AlreadyExists);

			RuleFor(x => x.FullNameVi)
				.NotEmpty()
				.WithMessage(CoreResource.Validation_msg_Required)
				.MaximumLength(255)
				.WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 255));

			RuleFor(x => x.FullNameEn)
				.NotEmpty()
				.WithMessage(CoreResource.Validation_msg_Required)
				.MaximumLength(255)
				.WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 255));

			RuleFor(x => x.FirstNameVi)
				.NotEmpty()
				.WithMessage(CoreResource.Validation_msg_Required)
				.MaximumLength(255)
				.WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 255));

			RuleFor(x => x.FirstNameEn)
				.NotEmpty()
				.WithMessage(CoreResource.Validation_msg_Required)
				.MaximumLength(255)
				.WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 255));

			RuleFor(x => x.LastNameVi)
				.NotEmpty()
				.WithMessage(CoreResource.Validation_msg_Required)
				.MaximumLength(255)
				.WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 255));

			RuleFor(x => x.LastNameEn)
				.NotEmpty()
				.WithMessage(CoreResource.Validation_msg_Required)
				.MaximumLength(255)
				.WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 255));

			RuleFor(x => x.Email)
				.EmailAddress()
				.WithMessage(CoreResource.Validation_msg_InvalidEmail)
				.MaximumLength(255)
				.WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 255));

			RuleFor(x => x.Phone)
				.Must(x => ValidationHelper.IsValidPhoneNumber(x))
				.WithMessage(CoreResource.Validation_msg_InvalidPhone)
				.MaximumLength(20)
				.WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 20));

			RuleFor(x => x.GenderCode)
				.MaximumLength(50)
				.WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 50));

			RuleFor(x => x.Images)
				.MaximumLength(1000)
				.WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 1000))
				.When(x => !string.IsNullOrEmpty(x.Images));

			RuleFor(x => x.StatusCode)
				.MaximumLength(50)
				.WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 50));

			RuleFor(x => x.TypeCode)
				.MaximumLength(50)
				.WithMessage(string.Format(CoreResource.Validation_msg_MaxLength, 50));
		}

		private bool IsValidCode(string tableName, string code)
		{
			using (DbContext dbContext = new DbContext())
			{
				var sql = $"SELECT COUNT(1) FROM {tableName} WHERE Code = @Code";
				var exists = dbContext.connection.ExecuteScalar<int>(sql, new { Code = code });
				return exists > 0;
			}
		}

		private bool IsExistsCode(string code)
		{
			using (DbContext dbContext = new DbContext())
			{
				var sql = @"SELECT COUNT(1) FROM ms_employees WHERE Code = @Code";
				var exists = dbContext.connection.ExecuteScalar<int>(sql, new { Code = code });
				return exists > 0;
			}
		}
	}
	#endregion

	#region Handler
	public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, ApiResponse<object>>
	{
		public async Task<CoreResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
		{
			CoreResponse response = new CoreResponse(CoreApiReturnCode.Succeed, CoreResource.Common_msg_Success);

			using (var dbContext = new DbContext(openTransaction: true))
			{
				try
				{
					var sql = @"
                        INSERT INTO [dbo].[employees] (
                            Code,
                            FullNameVi,
                            FullNameEn,
                            FirstNameVi,
                            FirstNameEn,
                            LastNameVi,
                            LastNameEn,
							Email,
							Phone,
							GenderCode,
							Images,
							StatusCode,
							TypeCode
                        ) VALUES (
                            @Code,
                            @FullNameVi,
                            @FullNameEn,
                            @FirstNameVi,
                            @FirstNameEn,
                            @LastNameVi,
							@LastNameEn,
							@Email,
							@Phone,
							@GenderCode,
							@Images,
							@StatusCode,
							@TypeCode                 
						)";

					var result = await dbContext.connection.ExecuteAsync(sql, request);
					await dbContext.transaction.CommitAsync();

					response = new CoreResponse(CoreApiReturnCode.Succeed, CoreResource.Common_msg_Success, result: result > 0);
				}
				catch (Exception)
				{
					await dbContext.transaction.RollbackAsync();
					throw;
				}
			}

			return await Task.FromResult(response);
		}
	}
	#endregion
}
