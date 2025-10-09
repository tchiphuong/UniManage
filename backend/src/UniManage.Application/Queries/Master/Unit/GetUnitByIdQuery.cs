using FluentValidation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Models;
using Dapper;

namespace UniManage.Application.Queries.Master.Unit
{
    public class GetUnitByIdQuery : IRequest<ApiResponse<GetUnitByIdQuery.Response>>
    {
        public int Id { get; set; }

        public class Response
        {
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string Code { get; set; } = string.Empty;
        }
    }

    public class GetUnitByIdQueryValidator : AbstractValidator<GetUnitByIdQuery>
    {
        public GetUnitByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0");
        }
    }

    public class GetUnitByIdQueryHandler : IRequestHandler<GetUnitByIdQuery, ApiResponse<GetUnitByIdQuery.Response>>
    {
        private static readonly string GetByIdSql = """
            SELECT Id, Name, Code
            FROM Units
            WHERE Id = @Id
            """;

        public async Task<ApiResponse<GetUnitByIdQuery.Response>> Handle(
            GetUnitByIdQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                using (var db = new DbContext())
                {
                    var unit = await db.connection.QueryFirstOrDefaultAsync<GetUnitByIdQuery.Response>(
                        GetByIdSql,
                        new { request.Id });

                    if (unit == null)
                    {
                        return ApiResponse<GetUnitByIdQuery.Response>.Fail(
                            "Unit not found");
                    }

                    Logger.LogInformation(
                        "Successfully retrieved unit {0}",
                        request.Id);

                    return ApiResponse<GetUnitByIdQuery.Response>.Success(unit);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex,
                    "Failed to get unit {0}",
                    request.Id);

                return ApiResponse<GetUnitByIdQuery.Response>.Fail(
                    "Failed to retrieve unit");
            }
        }
    }
}