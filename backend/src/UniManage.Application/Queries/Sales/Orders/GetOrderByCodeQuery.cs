using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.Sales.Orders
{
    public sealed class GetOrderByCodeQuery : BaseQuery, IRequest<ApiResponse<GetOrderByCodeQuery.Result>>
    {
        public string OrderCode { get; set; } = default!;

        public sealed class Result
        {
            public int Id { get; set; }
            public string OrderCode { get; set; } = default!;
            public string CustomerCode { get; set; } = default!;
            public string CustomerName { get; set; } = default!;
            public string EmployeeCode { get; set; } = default!;
            public string EmployeeName { get; set; } = default!;
            public DateTime? OrderDate { get; set; }
            public decimal? TotalAmount { get; set; }
            public string Status { get; set; } = default!;
            public DateTime? CreatedAt { get; set; }
            public List<OrderItemDto> Items { get; set; } = new();
        }

        public sealed class OrderItemDto
        {
            public int Id { get; set; }
            public string ItemCode { get; set; } = default!;
            public string ItemName { get; set; } = default!;
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal? TotalPrice { get; set; }
        }
    }

    public sealed class GetOrderByCodeQueryValidator : AbstractValidator<GetOrderByCodeQuery>
    {
        public GetOrderByCodeQueryValidator()
        {
            RuleFor(x => x.OrderCode)
                .NotEmpty().WithMessage("Order code is required");
        }
    }

    public sealed class GetOrderByCodeQueryHandler : IRequestHandler<GetOrderByCodeQuery, ApiResponse<GetOrderByCodeQuery.Result>>
    {
        public async Task<ApiResponse<GetOrderByCodeQuery.Result>> Handle(GetOrderByCodeQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.OrderCode), request.OrderCode)
                }
            };

            using (var dbContext = new DbContext())
            {
                try
                {
                    var orderSql = @"
                        SELECT 
                            o.Id,
                            o.OrderCode,
                            o.CustomerCode,
                            c.FullName AS CustomerName,
                            o.EmployeeCode,
                            e.FullName AS EmployeeName,
                            o.OrderDate,
                            o.TotalAmount,
                            o.Status,
                            o.CreatedAt
                        FROM sa_orders o
                        LEFT JOIN sa_customers c ON o.CustomerCode = c.CustomerCode
                        LEFT JOIN hr_employees e ON o.EmployeeCode = e.EmployeeCode
                        WHERE o.OrderCode = @OrderCode";

                    var order = await dbContext.QueryFirstOrDefaultAsync<GetOrderByCodeQuery.Result>(orderSql, new { OrderCode = request.OrderCode }, ct);

                    if (order == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetOrderByCodeQuery.Result>("Order not found");
                        log.ReturnCode = notFoundResponse.ReturnCode;
                        log.Message = notFoundResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return notFoundResponse;
                    }

                    var itemsSql = @"
                        SELECT 
                            oi.Id,
                            oi.ItemCode,
                            i.Name AS ItemName,
                            oi.Quantity,
                            oi.UnitPrice,
                            oi.TotalPrice
                        FROM sa_order_items oi
                        LEFT JOIN it_items i ON oi.ItemCode = i.Code
                        WHERE oi.OrderCode = @OrderCode";

                    var items = await dbContext.QueryAsync<GetOrderByCodeQuery.OrderItemDto>(itemsSql, new { OrderCode = request.OrderCode }, ct);
                    order.Items = items.ToList();

                    var response = ResponseHelper.Success(order, CoreResource.crud_getSuccess);

                    log.Result = order;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    UniLogger.Error($"Error retrieving order by code: {ex.Message}", ex);

                    var response = ResponseHelper.Error<GetOrderByCodeQuery.Result>("Error occurred while retrieving order");

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
