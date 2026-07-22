using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Database.Repositories;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyUser.Commands
{
    #region Command

    /// <summary>
    /// L?nh t?o m?i ngu?i d�ng trong h? th?ng
    /// </summary>
    public sealed class CreateUserCommand : BaseCommand, IRequest<ApiResponse<CreateUserCommand.Response>>
    {
        public string Username { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string? EmployeeCode { get; init; }
        public string Password { get; init; } = default!;
        public string Status { get; init; } = default!;
        public List<string> RoleCodes { get; init; } = default!;

        public sealed class Response
        {
            public bool Success => Id > 0;
            public long Id { get; init; }
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler for CreateUserCommand
    /// </summary>
    public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApiResponse<CreateUserCommand.Response>>
    {
        private readonly IMediator _mediator;

        public CreateUserCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ApiResponse<CreateUserCommand.Response>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            using (var dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    var passwordHash = PasswordHelper.HashPassword(request.Password);

                    var newUser = SyUsers.Create(
                        username: request.Username,
                        email: request.Email,
                        passwordHash: passwordHash,
                        status: request.Status,
                        createdBy: request.HeaderInfo?.Username ?? CoreConstant.SystemUser
                    );
                    newUser.EmployeeCode = request.EmployeeCode;
                    newUser.RoleCode = request.RoleCodes?.FirstOrDefault();

                    dbContext.Set<SyUsers>().Add(newUser);
                    await dbContext.SaveChangesAsync(cancellationToken);

                    // Assign roles to user
                    if (request.RoleCodes != null)
                    {
                        foreach (var role in request.RoleCodes)
                        {
                            dbContext.Set<SyUserRoles>().Add(new SyUserRoles
                            {
                                Username = newUser.Username,
                                RoleCode = role,
                                CreatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser,
                                CreatedAt = DateTimeHelper.Now
                            });
                        }
                        await dbContext.SaveChangesAsync(cancellationToken);
                    }

                    await dbContext.CommitAsync();

                    // Clear cache for combobox users since list has changed
                    await CacheHelper.RemoveByPatternAsync(CacheKeyConstant.System.ComboboxUsersPattern);

                    var responseData = new CreateUserCommand.Response { Id = newUser.Id };
                    return ResponseHelper.Success(responseData, string.Format(CoreResource.common_createSuccess, CoreResource.entity_user));
                }
                catch (Exception)
                {
                    await dbContext.RollbackAsync();
                    throw;
                }
            }
        }
    }

    #endregion
}


