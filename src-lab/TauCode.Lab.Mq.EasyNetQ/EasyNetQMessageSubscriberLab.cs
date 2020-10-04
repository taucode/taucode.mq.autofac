using System;
using System.Collections.Generic;
using TauCode.Mq;
using TauCode.Mq.EasyNetQ;

namespace TauCode.Lab.Mq.EasyNetQ
{
    public class EasyNetQMessageSubscriberLab : EasyNetQMessageSubscriber
    {
        public EasyNetQMessageSubscriberLab(IMessageHandlerContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        public EasyNetQMessageSubscriberLab(IMessageHandlerContextFactory contextFactory, IEnumerable<Type> handlerTypes)
            : base(contextFactory)
        {
            if (handlerTypes == null)
            {
                throw new ArgumentNullException(nameof(handlerTypes));
            }

            foreach (var handlerType in handlerTypes)
            {
                this.Subscribe(handlerType);
            }
        }

        public EasyNetQMessageSubscriberLab(IMessageHandlerContextFactory contextFactory, string connectionString)
            : base(contextFactory, connectionString)
        {
        }

        public EasyNetQMessageSubscriberLab(IMessageHandlerContextFactory contextFactory, string connectionString, IEnumerable<Type> handlerTypes)
            : base(contextFactory, connectionString)
        {
            if (handlerTypes == null)
            {
                throw new ArgumentNullException(nameof(handlerTypes));
            }

            foreach (var handlerType in handlerTypes)
            {
                this.Subscribe(handlerType);
            }
        }
    }
}
