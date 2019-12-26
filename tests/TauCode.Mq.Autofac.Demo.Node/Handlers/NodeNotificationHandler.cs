using System;
using TauCode.Mq.Autofac.Demo.All.Messages;

namespace TauCode.Mq.Autofac.Demo.Node.Handlers
{
    public class NodeNotificationHandler : MessageHandlerBase<Notification>
    {
        public override void Handle(Notification message)
        {
            throw new NotImplementedException();
        }
    }
}
