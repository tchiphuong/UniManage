using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.Master.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.Master.Application.Commands.Countries
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
        public async Task<ApiResponse<DeleteCountryCommand.Response>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            ApiLogModel logData = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new LogParamModel(nameof(request.Codes), string.Join(", ", request.Codes))
                }
            };

            using var dbContext = new DbContext(openTransaction: true);
            var countriesToDelete = await dbContext.Set<AdCountries>()
                .Where(x => request.Codes.Contains(x.Code))
                .ToListAsync(cancellationToken);

            var deletedCount = countriesToDelete.Count;
            if (deletedCount > 0)
            {
                dbContext.Set<AdCountries>().RemoveRange(countriesToDelete);
                await dbContext.SaveChangesAsync(cancellationToken);
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





