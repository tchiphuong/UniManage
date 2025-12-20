using Autofac;
using MediatR;
using UniManage.Application.Commands.System.Auth;

namespace UniManage.Api.Infrastructure.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(LoginCommand).Assembly;

            // Register MediatR Request Handlers
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces();

            // Register MediatR Notification Handlers
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(INotificationHandler<>))
                .AsImplementedInterfaces();
        }
    }
}
