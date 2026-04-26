using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Commands.Master.Countries
{
    #region Command

    /// <summary>
    /// Command to delete countries
    /// </summary>
    public sealed class DeleteCountryCommand : BaseCommand, IRequest<ApiResponse<DeleteCountryCommand.Response>>
    {
        public List<string> Codes { get; init; } = default!;

        public sealed class Response
        {
            public bool Success { get; init; }
            public int DeletedCount { get; init; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Validator for DeleteCountryCommand
    /// </summary>
    public sealed class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
    {
        public DeleteCountryCommandValidator()
        {
            RuleFor(x => x.Codes)
                .NotEmpty().WithMessage(CoreResource.validation_required)
                .Must(codes => codes.All(c => !string.IsNullOrEmpty(c)))
                .WithMessage("All codes must be non-empty");
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for DeleteCountryCommand
    /// </summary>
    public sealed class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, ApiResponse<DeleteCountryCommand.Response>>
    {
        public async Task<ApiResponse<DeleteCountryCommand.Response>> Handle(DeleteCountryCommand request, CancellationToken ct)
        {
            CoreLogModel logData = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Codes), string.Join(", ", request.Codes))
                }
            };

            using var dbContext = new DbContext(openTransaction: true);
            var countriesToDelete = await dbContext.Set<ad_countries>()
                .Where(x => request.Codes.Contains(x.Code))
                .ToListAsync(ct);

            var deletedCount = countriesToDelete.Count;
            if (deletedCount > 0)
            {
                dbContext.Set<ad_countries>().RemoveRange(countriesToDelete);
                await dbContext.SaveChangesAsync(ct);
            }

            await dbContext.CommitAsync();

            var responseData = new DeleteCountryCommand.Response { Success = true, DeletedCount = deletedCount };
            var response = ResponseHelper.Success(responseData, string.Format(CoreResource.common_deleteSuccess, CoreResource.entity_country, deletedCount));

            logData.Result = response.Data;
            logData.ReturnCode = response.ReturnCode;
            return response;
        }
    }

    #endregion
}

