using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SaCustomer.Commands
{
    public sealed class CreateCustomerCommand : BaseCommand, IRequest<ApiResponse<CreateCustomerCommand.Response>>
    {
        public string Code { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public string? Address { get; init; }
        public string? City { get; init; }
        public string? Country { get; init; }

        public sealed class Response
        {
            public int Id { get; init; }
        }
    }

    public sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Customer code is required")
                .Length(2, 50).WithMessage("Customer code must be between 2 and 50 characters")
                .Must(ValidationHelper.IsValidUserCode).WithMessage("Customer code allows only alphanumeric and underscore")
                .MustAsync(async (code, cancel) => !await IsCustomerCodeExistsAsync(code))
                .WithMessage("Customer code already exists");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Customer name is required")
                .Length(2, 255).WithMessage("Customer name must be between 2 and 255 characters");

            When(x => !string.IsNullOrEmpty(x.Email), () =>
            {
                RuleFor(x => x.Email)
                    .EmailAddress().WithMessage("Invalid email format");
            });

            When(x => !string.IsNullOrEmpty(x.Phone), () =>
            {
                RuleFor(x => x.Phone)
                    .Must(ValidationHelper.IsValidPhoneNumber!).WithMessage("Invalid phone number format");
            });
        }

        private static async Task<bool> IsCustomerCodeExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM sa_customers WHERE Code = @Code) THEN 1 ELSE 0 END",
                    new { Code = code });
            }
        }
    }

    public sealed class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ApiResponse<CreateCustomerCommand.Response>>
    {
        public async Task<ApiResponse<CreateCustomerCommand.Response>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        INSERT INTO sa_customers (Code, Name, Email, Phone, Address, City, Country, CreatedAt)
                        VALUES (@Code, @Name, @Email, @Phone, @Address, @City, @Country, GETDATE());
                        SELECT SCOPE_IDENTITY();";

                    var id = await dbContext.ExecuteScalarAsync<int>(sql, new
                    {
                        request.Code,
                        request.Name,
                        request.Email,
                        request.Phone,
                        request.Address,
                        request.City,
                        request.Country
                    }, cancellationToken);

                    

                    var responseData = new CreateCustomerCommand.Response { Id = id };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_createSuccess);
return response;
        }
    }
}




