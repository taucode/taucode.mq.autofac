using Newtonsoft.Json;
using System;
using TauCode.Mq.Autofac.Demo.All.Messages;

namespace TauCode.Mq.Autofac.Demo.Node.Handlers
{
    public class NodeGreetingHandler : MessageHandlerBase<Greeting>
    {
        private readonly IMessagePublisher _messagePublisher;

        public NodeGreetingHandler(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public override void Handle(Greeting message)
        {
            var json = JsonConvert.SerializeObject(message);
            Console.WriteLine(json);

            var response = new GreetingResponse(message, "Ciao! Tu hai scritto: " + message.Message);
            _messagePublisher.Publish(response, response.To);
        }
    }
}
