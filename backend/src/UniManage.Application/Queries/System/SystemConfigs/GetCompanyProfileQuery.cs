using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;
using FluentValidation;
using System.Linq;

namespace UniManage.Application.Queries.System.SystemConfigs
{
    #region Query

    public sealed class GetCompanyProfileQuery : BaseQuery, IRequest<ApiResponse<GetCompanyProfileQuery.Response>>
    {
        public sealed record Response
        {
            public string? CompanyName { get; set; }
            public string? CompanyAddress { get; set; }
            public string? CompanyPhone { get; set; }
            public string? CompanyEmail { get; set; }
            public string? CompanyTaxCode { get; set; }
            public string? CompanyLogo { get; set; }
        }
    }

    #endregion

    #region Validator

    public sealed class GetCompanyProfileQueryValidator : AbstractValidator<GetCompanyProfileQuery>
    {
        public GetCompanyProfileQueryValidator()
        {
        }
    }

    #endregion

    #region Handler

    public sealed class GetCompanyProfileQueryHandler : IRequestHandler<GetCompanyProfileQuery, ApiResponse<GetCompanyProfileQuery.Response>>
    {
        public async Task<ApiResponse<GetCompanyProfileQuery.Response>> Handle(GetCompanyProfileQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo);

            using (var dbContext = new DbContext())
            {
                try
                {
                    var configCodes = new[] { 
                        CoreConstant.ConfigCode.CompanyName, 
                        CoreConstant.ConfigCode.CompanyAddress, 
                        CoreConstant.ConfigCode.CompanyPhone, 
                        CoreConstant.ConfigCode.CompanyEmail, 
                        CoreConstant.ConfigCode.CompanyTaxCode, 
                        CoreConstant.ConfigCode.CompanyLogo 
                    };

                    var configs = await dbContext.Set<sy_configs>()
                        .Where(c => configCodes.Contains(c.ConfigCode))
                        .ToListAsync(ct);

                    var responseData = new GetCompanyProfileQuery.Response
                    {
                        CompanyName = configs.FirstOrDefault(c => c.ConfigCode == CoreConstant.ConfigCode.CompanyName)?.ConfigValue,
                        CompanyAddress = configs.FirstOrDefault(c => c.ConfigCode == CoreConstant.ConfigCode.CompanyAddress)?.ConfigValue,
                        CompanyPhone = configs.FirstOrDefault(c => c.ConfigCode == CoreConstant.ConfigCode.CompanyPhone)?.ConfigValue,
                        CompanyEmail = configs.FirstOrDefault(c => c.ConfigCode == CoreConstant.ConfigCode.CompanyEmail)?.ConfigValue,
                        CompanyTaxCode = configs.FirstOrDefault(c => c.ConfigCode == CoreConstant.ConfigCode.CompanyTaxCode)?.ConfigValue,
                        CompanyLogo = configs.FirstOrDefault(c => c.ConfigCode == CoreConstant.ConfigCode.CompanyLogo)?.ConfigValue
                    };

                    var response = ResponseHelper.Success(responseData, "Success");
                    log.ReturnCode = response.ReturnCode;
                    return response;
                }
                catch (Exception ex)
                {
                    log.IsException = true;
                    log.Message = ex.Message;
                    log.ReturnCode = UniManage.Core.Constant.CoreApiReturnCode.ExceptionOccurred;
                    return ResponseHelper.Error<GetCompanyProfileQuery.Response>(CoreResource.common_error);
                }
                finally
                {
                    UniLogManager.WriteApiLog(log);
                }
            }
        }
    }

    #endregion
}
