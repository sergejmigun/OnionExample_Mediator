using Autofac;
using MediatR;

namespace OnionExample.Domain.Services.DependencyInitialization
{
    public class ServicesImplementationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>)
            };

            foreach (var openHandlerType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(ThisAssembly)
                    .AsClosedTypesOf(openHandlerType)
                    .AsImplementedInterfaces();
            }

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}