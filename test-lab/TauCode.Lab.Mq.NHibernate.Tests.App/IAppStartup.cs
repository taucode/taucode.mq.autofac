using Autofac;

namespace TauCode.Lab.Mq.NHibernate.Tests.App
{
    public interface IAppStartup
    {
        ILifetimeScope AutofacContainer { get; }
        string ConnectionString { get; }
        string TempDbFilePath { get; }
    }
}
