using Dapper;
using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.Language
{
    #region Query
    public class GetLanguageListQuery : BaseQuery, IRequest<ApiResponse<PagedResult>>
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
    public class GetLanguageListQueryHandler : IRequestHandler<GetLanguageListQuery, ApiResponse<PagedResult>>
    {
        public async Task<ApiResponse<PagedResult>> Handle(GetLanguageListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse<PagedResult> response;
            CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
            logData.Parameter = new List<CoreParamModel>
            {
                new CoreParamModel { Name = "Keyword", Value = request.Keyword },
                new CoreParamModel { Name = "PageIndex", Value = request.PageIndex },
                new CoreParamModel { Name = "PageSize", Value = request.PageSize }
            };

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
                    var (orderByClause, _) = QueryHelper.BuildOrderByClause(request.SortBy, request.SortDirection, columnMappings);

                    #endregion

                    #region Execute

                    var result = await DatabaseHelper.QueryPagingAsync(
                        dbContext,
                        baseQuery,
                        whereClause,
                        orderByClause,
                        request,
                        request.PageIndex,
                        request.PageSize);

                    #endregion

                    response = ResponseHelper.Success(result, CoreResource.Common_msg_Success);
                    logData.Result = result;
                    logData.IsException = 0;
                    logData.ReturnCode = response.ReturnCode;
                }
                catch (Exception ex)
                {
                    response = ResponseHelper.Error<PagedResult>(CoreResource.Common_msg_ExceptionOccurred);
                    logData.Result = response.Data;
                    logData.Message = ex.ToString();
                    logData.IsException = 1;
                    logData.ReturnCode = response.ReturnCode;
                }
            }

            UniLogManager.WriteApiLog(logData);
            return response;
        }
    }
    #endregion
}
