using System;
using TauCode.Mq.Abstractions;

namespace TauCode.Mq.Autofac.Demo.All.Messages
{
    public class GreetingResponse : IMessage
    {
        // For serialization/deserialization
        public GreetingResponse()
        {
        }

        public GreetingResponse(Greeting greeting, string responseMessage)
        {
            this.CorrelationId = greeting.CorrelationId;
            this.CreatedAt = DateTime.UtcNow;

            this.To = greeting.From;
            this.From = greeting.To;
            this.OriginMessage = greeting.Message;
            this.ResponseMessage = responseMessage;
        }

        public string From { get; set; }
        public string To { get; set; }
        public string ResponseMessage { get; set; }
        public string OriginMessage { get; set; }

        public string CorrelationId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
