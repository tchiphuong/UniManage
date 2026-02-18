using Autofac;
using UniManage.Worker.Jobs;

namespace UniManage.Worker.Infrastructure.AutofacModules
{
    public class WorkerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(TokenCleanupJob).Assembly;

            // Register all Jobs in this assembly
            // Standard convention: Any class ending with "Job"
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Job"))
                .AsSelf()
                .InstancePerDependency();

            // Example: If you have services implementing interfaces
            // builder.RegisterAssemblyTypes(assembly)
            //     .Where(t => t.Name.EndsWith("Service"))
            //     .AsImplementedInterfaces()
            //     .InstancePerLifetimeScope();
        }
    }
}
