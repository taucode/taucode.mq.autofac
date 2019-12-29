using System;
using TauCode.Mq.Abstractions;
using TauCode.Mq.Autofac.Demo.All.Messages;

namespace TauCode.Mq.Autofac.Demo.Logger.Handlers
{
    public class LoggerNotificationHandler : MessageHandlerBase<Notification>
    {
        public override void Handle(Notification message)
        {
            throw new NotImplementedException();
        }
    }
}
