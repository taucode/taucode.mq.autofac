using Autofac;

namespace TauCode.Mq.Autofac;

public class AutofacMessageHandlerContext : IMessageHandlerContext
{
    #region Fields

    private readonly ILifetimeScope _contextLifetimeScope;

    #endregion

    #region Constructor

    public AutofacMessageHandlerContext(ILifetimeScope contextLifetimeScope)
    {
        _contextLifetimeScope = contextLifetimeScope ?? throw new ArgumentNullException(nameof(contextLifetimeScope));
    }

    #endregion

    #region IMessageHandlerContext Members

    public virtual Task BeginAsync(CancellationToken cancellationToken = default)
    {
        // idle
        return Task.CompletedTask;
    }

    public virtual object GetService(Type serviceType) => _contextLifetimeScope.Resolve(serviceType);

    public virtual Task EndAsync(CancellationToken cancellationToken = default)
    {
        // idle
        return Task.CompletedTask;
    }

    #endregion

    #region IDisposable Members

    public virtual void Dispose()
    {
        _contextLifetimeScope.Dispose();
    }

    #endregion
}