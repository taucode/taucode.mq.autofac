using System;
using Newtonsoft.Json;
using TauCode.Mq.Abstractions;
using TauCode.Mq.Autofac.Demo.All.Messages;

namespace TauCode.Mq.Autofac.Demo.Logger.Handlers
{
    public class LoggerGreetingResponseHandler : MessageHandlerBase<GreetingResponse>
    {
        public override void Handle(GreetingResponse message)
        {
            var json = JsonConvert.SerializeObject(message);
            Console.WriteLine(json);
        }
    }
}
