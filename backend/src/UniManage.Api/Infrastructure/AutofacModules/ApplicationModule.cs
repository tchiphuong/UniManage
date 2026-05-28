using Autofac;
using FluentValidation;
using MediatR;
using UniManage.Application.Commands.System.Auth;
using UniManage.Application.Services;
using UniManage.Core.Services.Social;

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

            // Register MediatR Pipeline Behaviors (Order matters: Outer -> Inner)
            // 0. Cache Behavior (Kiểm tra cache trước, giảm tải DB)
            builder.RegisterGeneric(typeof(UniManage.Application.Pipelines.CacheBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            // 1. Logging Behavior (Bắt trọn Request/Response/Exception)
            builder.RegisterGeneric(typeof(UniManage.Application.Pipelines.LoggingBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            // 2. Transaction Behavior (Đóng/mở kết nối cho Command)
            builder.RegisterGeneric(typeof(UniManage.Application.Pipelines.TransactionBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            // 3. Validation Behavior (Kiểm tra dữ liệu trước khi vào Handler)
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

            // ===========================================
            // [EXTENSIBILITY] Social Auth Architecture
            // Strategy Pattern for multi-provider support
            // ===========================================
            builder.RegisterType<GoogleAuthProvider>().As<ISocialAuthProvider>().InstancePerDependency();
            builder.RegisterType<FacebookAuthProvider>().As<ISocialAuthProvider>().InstancePerDependency();
            
            builder.RegisterType<SocialAuthProviderFactory>()
                .AsSelf()
                .SingleInstance();
        }
    }
}
