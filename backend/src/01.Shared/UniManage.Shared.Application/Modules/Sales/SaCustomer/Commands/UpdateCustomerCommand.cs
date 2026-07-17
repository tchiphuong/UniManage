using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SaCustomer.Commands
{
    public sealed class UpdateCustomerCommand : BaseCommand, IRequest<ApiResponse<UpdateCustomerCommand.Response>>
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
            public bool Success { get; init; }
        }
    }

    public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Customer code is required");

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
    }

    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ApiResponse<UpdateCustomerCommand.Response>>
    {
        public async Task<ApiResponse<UpdateCustomerCommand.Response>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        UPDATE sa_customers
                        SET Name = @Name,
                            Email = @Email,
                            Phone = @Phone,
                            Address = @Address,
                            City = @City,
                            Country = @Country
                        WHERE Code = @Code";

                    var rowsAffected = await dbContext.ExecuteAsync(sql, new
                    {
                        request.Code,
                        request.Name,
                        request.Email,
                        request.Phone,
                        request.Address,
                        request.City,
                        request.Country
                    }, cancellationToken);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(cancellationToken);
                        var errorResponse = ResponseHelper.Error<UpdateCustomerCommand.Response>("Customer not found");
return errorResponse;
                    }

                    

                    var responseData = new UpdateCustomerCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_updateSuccess);
return response;
        }
    }
}




