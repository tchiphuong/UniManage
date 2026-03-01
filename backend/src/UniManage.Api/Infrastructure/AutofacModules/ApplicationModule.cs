using Autofac;
using FluentValidation;
using MediatR;
using UniManage.Application.Commands.System.Auth;
using UniManage.Application.Services;

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

            // Register FluentValidation Validators
            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IValidator<>))
                .AsImplementedInterfaces();

            // Register MediatR Pipeline Behaviors
            builder.RegisterGeneric(typeof(UniManage.Application.Pipelines.ValidationBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            // ===========================================
            // [SECURITY] Register shared IdentityServer client (L1)
            // Centralizes all IdentityServer HTTP calls
            // Used by LoginCommand, RefreshTokenCommand, LogoutCommand
            // ===========================================
            builder.RegisterType<IdentityServerClient>()
                .As<IIdentityServerClient>()
                .SingleInstance();
        }
    }
}
