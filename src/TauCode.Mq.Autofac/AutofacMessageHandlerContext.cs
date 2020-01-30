using Autofac;
using System;

namespace TauCode.Mq.Autofac
{
    // todo clean
    public class AutofacMessageHandlerContext : IMessageHandlerContext
    {
        private readonly ILifetimeScope _contextLifetimeScope;

        public AutofacMessageHandlerContext(ILifetimeScope contextLifetimeScope)
        {
            _contextLifetimeScope = contextLifetimeScope ?? throw new ArgumentNullException(nameof(contextLifetimeScope));
            //this.ContextLifetimeScope =
            //    contextLifetimeScope ?? throw new ArgumentNullException(nameof(contextLifetimeScope));
        }

        //public ILifetimeScope ContextLifetimeScope { get; }

        public void Begin()
        {
            // idle
        }

        public void End()
        {
            // end
        }

        public object GetService(Type serviceType) => _contextLifetimeScope.Resolve(serviceType);

        public void Dispose()
        {
            _contextLifetimeScope.Dispose();
        }
    }
}
