namespace UniManage.Shared.Application.Modules.HrEmployeeShift.Commands
{
    public sealed class DeleteEmployeeShiftCommand : BaseCommand, IRequest<ApiResponse<DeleteEmployeeShiftCommand.Response>>
    {
        public List<int> Ids { get; init; } = new();

        public sealed class Response
        {
            public int DeletedCount { get; init; }
        }
    }

    public sealed class DeleteEmployeeShiftCommandValidator : AbstractValidator<DeleteEmployeeShiftCommand>
    {
        public DeleteEmployeeShiftCommandValidator()
        {
            RuleFor(x => x.Ids)
                .NotEmpty().WithMessage("Ids are required")
                .Must(ids => ids.All(id => id > 0)).WithMessage("All Ids must be greater than 0");
        }
    }

    public sealed class DeleteEmployeeShiftCommandHandler : IRequestHandler<DeleteEmployeeShiftCommand, ApiResponse<DeleteEmployeeShiftCommand.Response>>
    {
        public async Task<ApiResponse<DeleteEmployeeShiftCommand.Response>> Handle(DeleteEmployeeShiftCommand request, CancellationToken cancellationToken)
        {
            

            using var dbContext = new DbContext(openTransaction: true);
                    var sql = @"
                        DELETE FROM HrEmployeeShifts
                        WHERE Id IN @Ids";

                    var deletedCount = await dbContext.ExecuteAsync(sql, new { Ids = request.Ids }, cancellationToken);

                    

                    var responseData = new DeleteEmployeeShiftCommand.Response { DeletedCount = deletedCount };
                    var response = ResponseHelper.Success(responseData, CoreResource.common_deleteSuccess);
return response;
        }
    }
}





