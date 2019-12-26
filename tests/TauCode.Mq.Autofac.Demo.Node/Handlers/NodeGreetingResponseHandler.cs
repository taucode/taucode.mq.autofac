using Newtonsoft.Json;
using System;
using TauCode.Mq.Autofac.Demo.All.Messages;

namespace TauCode.Mq.Autofac.Demo.Node.Handlers
{
    public class NodeGreetingResponseHandler : MessageHandlerBase<GreetingResponse>
    {
        public override void Handle(GreetingResponse message)
        {
            var json = JsonConvert.SerializeObject(message);
            Console.WriteLine(json);
        }
    }
}
