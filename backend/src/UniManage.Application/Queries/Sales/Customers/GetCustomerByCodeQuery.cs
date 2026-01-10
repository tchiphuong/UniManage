using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Sales.Customers
{
    public sealed class GetCustomerByCodeQuery : BaseQuery, IRequest<ApiResponse<GetCustomerByCodeQuery.Result>>
    {
        public string Code { get; set; } = default!;

        public sealed class Result
        {
            public int Id { get; set; }
            public string Code { get; set; } = default!;
            public string Name { get; set; } = default!;
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? Address { get; set; }
            public string? City { get; set; }
            public string? Country { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }

    public sealed class GetCustomerByCodeQueryValidator : AbstractValidator<GetCustomerByCodeQuery>
    {
        public GetCustomerByCodeQueryValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Customer code is required");
        }
    }

    public sealed class GetCustomerByCodeQueryHandler : IRequestHandler<GetCustomerByCodeQuery, ApiResponse<GetCustomerByCodeQuery.Result>>
    {
        public async Task<ApiResponse<GetCustomerByCodeQuery.Result>> Handle(GetCustomerByCodeQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Code), request.Code)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var sql = @"
                        SELECT 
                            Id,
                            Code,
                            Name,
                            Email,
                            Phone,
                            Address,
                            City,
                            Country,
                            CreatedAt
                        FROM sa_customers
                        WHERE Code = @Code";

                    var result = await dbContext.QueryFirstOrDefaultAsync<GetCustomerByCodeQuery.Result>(sql, new { request.Code }, ct);

                    if (result == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetCustomerByCodeQuery.Result>("Customer not found");
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundResponse;
                    }

                    var response = ResponseHelper.Success(result, CoreResource.Common_msg_GetSuccess);

                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving customer by code: {ex.Message}", ex);

                    var response = ResponseHelper.Error<GetCustomerByCodeQuery.Result>("Error occurred while retrieving customer");

                    log.IsException = 1;
                    log.Message = ex.Message;
                    log.ReturnCode = response.ReturnCode;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
        }
    }
}
