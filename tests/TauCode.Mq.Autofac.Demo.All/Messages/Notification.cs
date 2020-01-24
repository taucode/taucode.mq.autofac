using System;
using TauCode.Mq.Abstractions;

namespace TauCode.Mq.Autofac.Demo.All.Messages
{
    public class Notification : IMessage
    {
        // For serialization/deserialization
        public Notification()
        {
            throw new NotImplementedException(); // todo
        }

        public Notification(string correlationId)
        {
            throw new NotImplementedException(); // todo
        }

        public string CorrelationId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
