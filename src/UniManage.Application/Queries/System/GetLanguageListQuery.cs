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

namespace UniManage.Api.Domains.Query.System
{
    #region Query
    public class GetLanguageListQuery : CoreBaseQuery, IRequest<CoreResponse>
    {
        public string? Keyword { get; set; }
        public string? StatusCode { get; set; }

        public class Result
        {
            public string? LanguageCode { get; set; }
            public string? LanguageName { get; set; }
            public string? Icon { get; set; }
            public bool IsDefault { get; set; }
            public bool IsActive { get; set; }
        }
    }
    #endregion

    #region Validator
    public class GetLanguageListQueryValidator : AbstractValidator<GetLanguageListQuery>
    {
        public GetLanguageListQueryValidator()
        {
            RuleFor(x => x.StatusCode)
                .Must(x => x == null || IsValidCode("ms_status", x))
                .WithMessage(CoreResource.Validation_msg_AlreadyExists)
                .When(x => !string.IsNullOrEmpty(x.StatusCode));

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
    public class GetLanguageListQueryHandler : IRequestHandler<GetLanguageListQuery, CoreResponse>
    {
        public async Task<CoreResponse> Handle(GetLanguageListQuery request, CancellationToken cancellationToken)
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
                            LanguageCode,
                            LanguageName,
                            Icon,
                            IsDefault,
                            IsActive
                        FROM sy_languages";

                    #endregion

                    #region Filter condition

                    var conditions = new StringBuilder();

                    #endregion

                    #region Sort by

                    var columnMappings = new Dictionary<string, string>
                    {
                        { "LanguageCode", "LanguageCode" },
                        { "LanguageName", "LanguageName" },
                    };

                    string whereClause = conditions.Length > 0 ? $"WHERE 1 = 1 {conditions}" : string.Empty;
                    var (orderByClause, _) = QueryHelper.BuildOrderByClause(request.SortBy, request.SortDirection ?? "ASC", columnMappings);

                    #endregion

                    #region Execute

                    var (results, paging) = await dbContext.connection.QueryPagingAsync<GetLanguageListQuery.Result>(
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
