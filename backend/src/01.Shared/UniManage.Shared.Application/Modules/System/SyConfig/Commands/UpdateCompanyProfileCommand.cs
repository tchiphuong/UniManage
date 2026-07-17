using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyConfig.Commands
{
    #region Command

    public sealed class UpdateCompanyProfileCommand : BaseCommand, IRequest<ApiResponse<bool>>
    {
        public string? CompanyName { get; init; }
        public string? CompanyAddress { get; init; }
        public string? CompanyPhone { get; init; }
        public string? CompanyEmail { get; init; }
        public string? CompanyTaxCode { get; init; }
        public string? CompanyLogo { get; init; }
    }

    #endregion

    #region Validator

    public sealed class UpdateCompanyProfileCommandValidator : AbstractValidator<UpdateCompanyProfileCommand>
    {
        public UpdateCompanyProfileCommandValidator()
        {
            RuleFor(x => x.CompanyEmail)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.CompanyEmail))
                .WithMessage(CoreResource.validation_invalidFormat);
        }
    }

    #endregion

    #region Handler

    public sealed class UpdateCompanyProfileCommandHandler : IRequestHandler<UpdateCompanyProfileCommand, ApiResponse<bool>>
    {
        public async Task<ApiResponse<bool>> Handle(UpdateCompanyProfileCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameters = new List<LogParamModel>
                {
                    new(nameof(request.CompanyName), request.CompanyName),
                    new(nameof(request.CompanyAddress), request.CompanyAddress),
                    new(nameof(request.CompanyPhone), request.CompanyPhone),
                    new(nameof(request.CompanyEmail), request.CompanyEmail),
                    new(nameof(request.CompanyTaxCode), request.CompanyTaxCode),
                    new(nameof(request.CompanyLogo), request.CompanyLogo)
                }
            };

            var username = request.HeaderInfo?.Username;
            if (string.IsNullOrEmpty(username))
            {
                return ResponseHelper.Error<bool>(CoreResource.common_unauthorized);
            }

            try
            {
                using var dbContext = new DbContext(openTransaction: true);

                var configCodes = new[] { 
                    CoreConstant.ConfigCode.CompanyName, 
                    CoreConstant.ConfigCode.CompanyAddress, 
                    CoreConstant.ConfigCode.CompanyPhone, 
                    CoreConstant.ConfigCode.CompanyEmail, 
                    CoreConstant.ConfigCode.CompanyTaxCode, 
                    CoreConstant.ConfigCode.CompanyLogo 
                };
                
                var configs = await dbContext.Set<SyConfigs>()
                    .Where(c => configCodes.Contains(c.ConfigCode))
                    .ToListAsync(cancellationToken);

                // Helper local function to update or add config
                void UpdateOrAddConfig(string code, string? value)
                {
                    if (value == null) return;
                    
                    var config = configs.FirstOrDefault(c => c.ConfigCode == code);
                    if (config != null)
                    {
                        config.ConfigValue = value;
                        config.UpdatedBy = username;
                        config.UpdatedAt = DateTimeHelper.Now;
                    }
                    else
                    {
                        dbContext.Set<SyConfigs>().Add(new SyConfigs
                        {
                            Uuid = Guid.CreateVersion7(),
                            ConfigCode = code,
                            ConfigName = code,
                            ConfigValue = value,
                            DataType = "STRING",
                            IsSystem = false,
                            CreatedBy = username,
                            CreatedAt = DateTimeHelper.Now,
                            DataRowVersion = new byte[8]
                        });
                    }
                }

                UpdateOrAddConfig(CoreConstant.ConfigCode.CompanyName, request.CompanyName);
                UpdateOrAddConfig(CoreConstant.ConfigCode.CompanyAddress, request.CompanyAddress);
                UpdateOrAddConfig(CoreConstant.ConfigCode.CompanyPhone, request.CompanyPhone);
                UpdateOrAddConfig(CoreConstant.ConfigCode.CompanyEmail, request.CompanyEmail);
                UpdateOrAddConfig(CoreConstant.ConfigCode.CompanyTaxCode, request.CompanyTaxCode);
                UpdateOrAddConfig(CoreConstant.ConfigCode.CompanyLogo, request.CompanyLogo);

                await dbContext.SaveChangesAsync(cancellationToken);
                await dbContext.CommitAsync();

                var response = ResponseHelper.Success(true, CoreResource.common_updateSuccess);
                log.ReturnCode = response.ReturnCode;
                return response;
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = UniManage.Shared.Infrastructure.Constant.CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<bool>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}






