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

        public IMessageHandlerContext CreateContext()
        {
            var childScope = _rootLifetimeScope.BeginLifetimeScope();
            var context = new AutofacMessageHandlerContext(childScope);
            return context;
        }

        public IMessageHandler CreateHandler(IMessageHandlerContext context, Type handlerType)
        {
            var autofacMessageHandlerContext = (AutofacMessageHandlerContext)context; // todo check this
            var scope = autofacMessageHandlerContext.ContextLifetimeScope;
            var handler = (IMessageHandler)scope.Resolve(handlerType);
            return handler;
        }
    }
}
