using System;
using Autofac;

namespace TauCode.Mq.Autofac
{
    public class AutofacMessageHandlerContextFactory : IMessageHandlerContextFactory
    {
        #region Constructor

        public AutofacMessageHandlerContextFactory(ILifetimeScope rootLifetimeScope)
        {
            this.RootLifetimeScope = rootLifetimeScope ?? throw new ArgumentNullException(nameof(rootLifetimeScope));
        }

        #endregion

        #region Protected

        protected ILifetimeScope RootLifetimeScope { get; }

        #endregion
        
        #region IMessageHandlerContextFactory Members

        public virtual IMessageHandlerContext CreateContext()
        {
            var childScope = this.RootLifetimeScope.BeginLifetimeScope();
            var context = new AutofacMessageHandlerContext(childScope);
            return context;
        }

        #endregion
    }
}
