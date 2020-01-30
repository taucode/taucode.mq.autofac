using Autofac;
using System;

namespace TauCode.Mq.Autofac
{
    public class AutofacMessageHandlerContextFactory : IMessageHandlerContextFactory
    {
        private readonly ILifetimeScope _rootLifetimeScope;

        public AutofacMessageHandlerContextFactory(ILifetimeScope rootLifetimeScope)
        {
            _rootLifetimeScope = rootLifetimeScope ?? throw new ArgumentNullException(nameof(rootLifetimeScope));
        }

        public virtual IMessageHandlerContext CreateContext()
        {
            var childScope = _rootLifetimeScope.BeginLifetimeScope();
            var context = new AutofacMessageHandlerContext(childScope);
            return context;
        }
    }
}
