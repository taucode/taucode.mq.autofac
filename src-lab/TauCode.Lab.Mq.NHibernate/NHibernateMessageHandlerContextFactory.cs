using Autofac;
using TauCode.Mq;
using TauCode.Mq.Autofac;

namespace TauCode.Lab.Mq.NHibernate
{
    public class NHibernateMessageHandlerContextFactory : AutofacMessageHandlerContextFactory
    {
        public NHibernateMessageHandlerContextFactory(ILifetimeScope rootLifetimeScope)
            : base(rootLifetimeScope)
        {
        }

        public override IMessageHandlerContext CreateContext()
        {
            var childScope = this.RootLifetimeScope.BeginLifetimeScope();
            var context = new NHibernateMessageHandlerContext(childScope);
            return context;
        }
    }
}
