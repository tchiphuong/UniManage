using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Extensions;
using UniManage.Core.Helpers;
using UniManage.Core.Logging;
using UniManage.Core.Models;
using UniManage.Resource;

namespace UniManage.Api.Domains.Query.Master.Employee
{
    #region Query
    public class GetEmployeeListQuery : CoreBaseQuery, IRequest<CoreResponse>
    {
        public string? Keyword { get; set; }
        public string? GenderCode { get; set; }
        public string? StatusCode { get; set; }
        public string? TypeCode { get; set; }
        public class Result
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
            public int TotalCount { get; set; }
        }
    }
    #endregion

    #region Validator
    public class GetEmployeeListQueryValidator : AbstractValidator<GetEmployeeListQuery>
    {
        public GetEmployeeListQueryValidator()
        {
            RuleFor(x => x.GenderCode)
                .Must(x => x == null || IsValidCode("ms_genders", x))
                .WithMessage(CoreResource.Validation_msg_AlreadyExists)
                .When(x => !string.IsNullOrEmpty(x.GenderCode));

            RuleFor(x => x.StatusCode)
                .Must(x => x == null || IsValidCode("ms_status", x))
                .WithMessage(CoreResource.Validation_msg_AlreadyExists)
                .When(x => !string.IsNullOrEmpty(x.StatusCode));

            RuleFor(x => x.TypeCode)
                .Must(x => x == null || IsValidCode("ms_types", x))
                .WithMessage(CoreResource.Validation_msg_AlreadyExists)
                .When(x => !string.IsNullOrEmpty(x.TypeCode));

            RuleFor(x => x.PageIndex)
                .GreaterThan(0)
                .WithMessage(CoreResource.Validation_msg_Required);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage(CoreResource.Validation_msg_Required);

            RuleFor(x => x.SortDirection)
                .Must(x => x == null || x.ToUpper() == "ASC" || x.ToUpper() == "DESC")
                .WithMessage(CoreResource.Validation_msg_AlreadyExists);
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
    }
    #endregion

    #region Handler
    public class GetEmployeeListQueryHandler : IRequestHandler<GetEmployeeListQuery, CoreResponse>
    {
        public async Task<CoreResponse> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            CoreResponse response;
            var logData = new CoreLogModel(request.HeaderInfo);

            using (var dbContext = new DbContext())
            {
                try
                {
                    #region SQL query

                    var baseQuery = @"
                        SELECT 
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
                        FROM ms_employees";

                    #endregion

                    #region Filter condition

                    var conditions = new StringBuilder();

                    if (!string.IsNullOrEmpty(request.GenderCode))
                    {
                        conditions.Append(" AND GenderCode = @GenderCode");
                    }

                    if (!string.IsNullOrEmpty(request.StatusCode))
                    {
                        conditions.Append(" AND StatusCode = @StatusCode");
                    }

                    if (!string.IsNullOrEmpty(request.TypeCode))
                    {
                        conditions.Append(" AND TypeCode = @TypeCode");
                    }

                    #endregion

                    #region Sort by

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "Code", "Code" },
                        { "FullNameVi", "FullNameVi" },
                        { "FullNameEn", "FullNameEn" },
                        { "Email", "Email" },
                    };

                    string whereClause = conditions.Length > 0 ? $"WHERE 1 = 1 {conditions}" : string.Empty;
                    var (orderByClause, _) = QueryHelper.BuildOrderByClause(request.SortBy, request.SortDirection ?? "ASC", columnMappings);

                    #endregion

                    #region Excute

                    var (results, paging) = await dbContext.connection.QueryPagingAsync<GetEmployeeListQuery.Result>(
                        new DapperExtensions.SqlBuilder
                        {
                            BaseQuery = baseQuery,
                            WhereClause = whereClause,
                            OrderByClause = orderByClause,
                            Parameters = request
                        },
                        request.PageIndex,
                        request.PageSize);

                    #endregion

                    response = new CoreResponse(CoreApiReturnCode.Succeed, CoreResource.Common_msg_Success, results, paging);
                    logData.Result = results;
                    logData.IsException = 0;
                    logData.ReturnCode = response.ReturnCode;
                }
                catch (Exception ex)
                {
                    response = new CoreResponse(CoreApiReturnCode.ExceptionOccurred, CoreResource.Common_msg_ExceptionOccurred);
                    logData.Result = response.Data;
                    logData.Message = ex.ToString();
                    logData.IsException = 1;
                    logData.ReturnCode = response.ReturnCode;
                }
            }

            UniLogManager.WriteApiLog(logData);
            return await Task.FromResult(response);
        }
    }
    #endregion
}