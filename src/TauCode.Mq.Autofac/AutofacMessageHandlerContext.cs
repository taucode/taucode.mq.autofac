using Autofac;
using System;

namespace TauCode.Mq.Autofac
{
    public class AutofacMessageHandlerContext : IMessageHandlerContext
    {
        public AutofacMessageHandlerContext(ILifetimeScope contextLifetimeScope)
        {
            this.ContextLifetimeScope =
                contextLifetimeScope ?? throw new ArgumentNullException(nameof(contextLifetimeScope));
        }

        public ILifetimeScope ContextLifetimeScope { get; }

        public void Begin()
        {
            // idle
        }

        public void End()
        {
            // end
        }

        public void Dispose()
        {
            this.ContextLifetimeScope?.Dispose();
        }
    }
}
