using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Sales.Customers
{
    public sealed class DeleteCustomerCommand : BaseCommand, IRequest<ApiResponse<DeleteCustomerCommand.Response>>
    {
        public List<string> Codes { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    public sealed class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x => x.Codes)
                .NotEmpty().WithMessage("Customer codes are required")
                .Must(codes => codes.All(code => !string.IsNullOrEmpty(code)))
                .WithMessage("All customer codes must be valid");

            RuleFor(x => x.Codes)
                .MustAsync(async (codes, cancel) => !await IsAnyCustomerHasOrdersAsync(codes))
                .WithMessage("One or more customers have orders and cannot be deleted");
        }

        private static async Task<bool> IsAnyCustomerHasOrdersAsync(List<string> codes)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.ExecuteScalarAsync<bool>(
                    "SELECT CASE WHEN EXISTS(SELECT 1 FROM sa_orders WHERE CustomerCode IN @Codes) THEN 1 ELSE 0 END",
                    new { Codes = codes });
            }
        }
    }

    public sealed class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ApiResponse<DeleteCustomerCommand.Response>>
    {
        public async Task<ApiResponse<DeleteCustomerCommand.Response>> Handle(DeleteCustomerCommand request, CancellationToken ct)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        DELETE FROM sa_customers
                        WHERE Code IN @Codes";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { Codes = request.Codes }, ct);

                    

                    var responseData = new DeleteCustomerCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
return response;
        }
    }
}


