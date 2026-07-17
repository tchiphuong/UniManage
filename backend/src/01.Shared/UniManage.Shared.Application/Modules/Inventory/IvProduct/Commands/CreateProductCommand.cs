using FluentValidation;
using MediatR;
using Newtonsoft.Json;
using UniManage.Shared.Domain.Entities;

namespace UniManage.Shared.Application.Modules.IvProduct.Commands
{
    #region Command
    /// <summary>
    /// Lệnh tạo sản phẩm mới
    /// </summary>
    public sealed class CreateProductCommand : BaseCommand, IRequest<ApiResponse<CreateProductCommand.Response>>
    {
        public string Code { get; init; } = string.Empty;
        public string NameLocalized { get; init; } = "{}";
        public decimal Price { get; init; }
        public Guid CategoryId { get; init; }

        public sealed class Response
        {
            public Guid Id { get; init; }
        }
    }
    #endregion

    #region Validator
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("validation_required|lbl_productcode")
                .MaximumLength(50).WithMessage("validation_range|lbl_productcode|1|50");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("validation_min|lbl_price|0");

            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("validation_required|lbl_categoryid");
        }
    }
    #endregion

    #region Handler
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<CreateProductCommand.Response>>
    {
        private readonly UniManage.Shared.Infrastructure.Database.DbContext _context;

        public CreateProductCommandHandler(UniManage.Shared.Infrastructure.Database.DbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponse<CreateProductCommand.Response>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>()
            };

            try
            {
                var product = new INV_Product
                {
                    Code = request.Code,
                    NameLocalized = request.NameLocalized,
                    Price = request.Price,
                    CategoryId = request.CategoryId,
                    CreatedByUserId = Guid.TryParse(request.HeaderInfo?.UserId, out var uid) ? uid : null
                };

                _context.Set<INV_Product>().Add(product);
                await _context.SaveChangesAsync(cancellationToken);

                var responseData = new CreateProductCommand.Response { Id = product.Id };
                var response = ResponseHelper.Success(responseData, "Tạo sản phẩm thành công");
                
                log.Result = JsonConvert.SerializeObject(response);
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                return response;
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<CreateProductCommand.Response>(ex.Message);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }
    #endregion
}






