using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;
using DbContext = UniManage.Core.Database.DbContext;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Core.Constant;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;

namespace UniManage.Application.Commands.HR.Positions
{
    #region Command

    /// <summary>
    /// Lệnh tạo mới chức vụ
    /// </summary>
    public sealed class CreatePositionCommand : BaseCommand, IRequest<ApiResponse<CreatePositionCommand.Response>>
    {
        public string PositionCode { get; init; } = default!;
        public string NameVi { get; init; } = default!;
        public string NameEn { get; init; } = default!;
        public string? Description { get; init; }

        public sealed class Response
        {
            public bool Success => Id > 0;
            public int Id { get; init; }
            public string PositionCode { get; init; } = default!;
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh tạo chức vụ
    /// </summary>
    public sealed class CreatePositionCommandValidator : AbstractValidator<CreatePositionCommand>
    {
        public CreatePositionCommandValidator()
        {
            RuleFor(x => x.PositionCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_code))
                .MaximumLength(50).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_code, 50))
                .Matches("^[A-Z0-9_]+$").WithMessage(CoreResource.validation_alphanumericOnly)
                .MustAsync(async (code, cancel) => !await IsCodeExistsAsync(code))
                .WithMessage(CoreResource.validation_alreadyExists);

            RuleFor(x => x.NameVi)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_name))
                .MaximumLength(200).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_name, 200));

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_name))
                .MaximumLength(200).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_name, 200));

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.common_description, 500));
        }

        private static async Task<bool> IsCodeExistsAsync(string code)
        {
            using (var dbContext = new DbContext())
            {
                return await dbContext.Set<hr_positions>().AnyAsync(x => x.Code == code);
            }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lệnh tạo mới chức vụ
    /// </summary>
    public sealed class CreatePositionCommandHandler : IRequestHandler<CreatePositionCommand, ApiResponse<CreatePositionCommand.Response>>
    {
        public async Task<ApiResponse<CreatePositionCommand.Response>> Handle(CreatePositionCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new(nameof(request.PositionCode), request.PositionCode),
                    new(nameof(request.NameVi), request.NameVi),
                    new(nameof(request.NameEn), request.NameEn)
                }
            };

            try
            {
                using var dbContext = new DbContext(openTransaction: true);

                var position = new hr_positions
                {
                    Code = request.PositionCode,
                    NameVi = request.NameVi,
                    NameEn = request.NameEn,
                    Description = request.Description ?? string.Empty,
                    CreatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser,
                    CreatedAt = DateTimeHelper.Now
                };

                dbContext.Set<hr_positions>().Add(position);
                await dbContext.SaveChangesAsync(ct);
                await dbContext.CommitAsync();

                var responseData = new CreatePositionCommand.Response
                {
                    Id = position.Id,
                    PositionCode = position.Code
                };

                var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_createSuccess, CoreResource.entity_position));
                
                log.Result = responseData;
                log.Message = response.Message;
                log.ReturnCode = response.ReturnCode;

                return response;
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<CreatePositionCommand.Response>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}
