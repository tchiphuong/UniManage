using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using FluentValidation;
using MediatR;
using UniManage.Shared.Infrastructure.Services;

namespace UniManage.WebApi.Infrastructure
{
    public class ApplicationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "UniManage.Modules.*.Application.dll")
                .Select(Assembly.LoadFrom)
                .ToArray();

            // Register MediatR Request Handlers
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces();

            // Register MediatR Notification Handlers
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(INotificationHandler<>))
                .AsImplementedInterfaces();

            // Register FluentValidation Validators
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(IValidator<>))
                .AsImplementedInterfaces();

            // Register MediatR Pipeline Behaviors (Order matters: Outer -> Inner)
            // 0. Cache Behavior (Kiểm tra cache trước, giảm tải DB)
            builder.RegisterGeneric(typeof(UniManage.Shared.Infrastructure.Pipelines.CacheBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            // 1. Logging Behavior (Bắt trọn Request/Response/Exception)
            builder.RegisterGeneric(typeof(UniManage.Shared.Infrastructure.Pipelines.LoggingBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            // 2. Transaction Behavior (Đóng/mở kết nối cho Command)
            builder.RegisterGeneric(typeof(UniManage.Shared.Infrastructure.Pipelines.TransactionBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            // 3. Validation Behavior (Kiểm tra dữ liệu trước khi vào Handler)
            builder.RegisterGeneric(typeof(UniManage.Shared.Infrastructure.Pipelines.ValidationBehavior<,>))
                .As(typeof(IPipelineBehavior<,>));

            // ===========================================
            // [SECURITY] Register shared IdentityServer client (L1)
            // Centralizes all IdentityServer HTTP calls
            // Used by LoginCommand, RefreshTokenCommand, LogoutCommand
            // ===========================================
            builder.RegisterType<IdentityServerClient>()
                .As<UniManage.Shared.Application.Interfaces.IIdentityServerClient>()
                .SingleInstance();

            // ===========================================
            // [EXTENSIBILITY] Social Auth Architecture
            // Strategy Pattern for multi-provider support
            // Sử dụng fully-qualified name để tránh ambiguous reference
            // ===========================================
            builder.RegisterType<UniManage.Modules.System.Infrastructure.Services.Social.GoogleAuthProvider>()
                .As<UniManage.Shared.Application.Interfaces.ISocialAuthProvider>()
                .InstancePerDependency();
            builder.RegisterType<UniManage.Modules.System.Infrastructure.Services.Social.FacebookAuthProvider>()
                .As<UniManage.Shared.Application.Interfaces.ISocialAuthProvider>()
                .InstancePerDependency();
            
            builder.RegisterType<UniManage.Shared.Infrastructure.Services.SocialAuthProviderFactory>()
                .AsSelf()
                .SingleInstance();
        }
    }
}
