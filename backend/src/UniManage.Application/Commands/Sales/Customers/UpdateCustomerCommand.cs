using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Commands.Sales.Customers
{
    public sealed class UpdateCustomerCommand : BaseCommand, IRequest<ApiResponse<UpdateCustomerCommand.Response>>
    {
        public string Code { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string? Email { get; init; }
        public string? Phone { get; init; }
        public string? Address { get; init; }
        public string? City { get; init; }
        public string? Country { get; init; }

        public sealed class Response
        {
            public bool Success { get; init; }
        }
    }

    public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Customer code is required");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Customer name is required")
                .Length(2, 255).WithMessage("Customer name must be between 2 and 255 characters");

            When(x => !string.IsNullOrEmpty(x.Email), () =>
            {
                RuleFor(x => x.Email)
                    .EmailAddress().WithMessage("Invalid email format");
            });

            When(x => !string.IsNullOrEmpty(x.Phone), () =>
            {
                RuleFor(x => x.Phone)
                    .Must(ValidationHelper.IsValidPhoneNumber!).WithMessage("Invalid phone number format");
            });
        }
    }

    public sealed class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ApiResponse<UpdateCustomerCommand.Response>>
    {
        public async Task<ApiResponse<UpdateCustomerCommand.Response>> Handle(UpdateCustomerCommand request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Code), request.Code),
                    new CoreParamModel(nameof(request.Name), request.Name),
                    new CoreParamModel(nameof(request.Email), request.Email),
                    new CoreParamModel(nameof(request.Phone), request.Phone)
                }
            };

            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var sql = @"
                        UPDATE sa_customers
                        SET Name = @Name,
                            Email = @Email,
                            Phone = @Phone,
                            Address = @Address,
                            City = @City,
                            Country = @Country
                        WHERE Code = @Code";

                    var rowsAffected = await dbContext.ExecuteAsync(sql, new
                    {
                        request.Code,
                        request.Name,
                        request.Email,
                        request.Phone,
                        request.Address,
                        request.City,
                        request.Country
                    }, ct);

                    if (rowsAffected == 0)
                    {
                        await dbContext.RollbackAsync(ct);
                        var errorResponse = ResponseHelper.Error<UpdateCustomerCommand.Response>("Customer not found");
                        log.ReturnCode = errorResponse.ReturnCode;
                        log.Message = errorResponse.Message;
                        UniLogManager.WriteApiLog(log);
                        return errorResponse;
                    }

                    await dbContext.CommitAsync(ct);

                    var responseData = new UpdateCustomerCommand.Response { Success = true };
                    var response = ResponseHelper.Success(responseData, CoreResource.Common_msg_UpdateSuccess);

                    log.Result = response;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
                catch (Exception ex)
                {
                    await dbContext.RollbackAsync(ct);
                    UniLogger.Error($"Error updating customer: {ex.Message}", ex);

                    var response = ResponseHelper.Error<UpdateCustomerCommand.Response>("Error occurred while updating customer");

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
