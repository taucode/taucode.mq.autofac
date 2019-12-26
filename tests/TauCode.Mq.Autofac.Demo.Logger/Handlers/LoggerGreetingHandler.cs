using System;
using Newtonsoft.Json;
using TauCode.Mq.Autofac.Demo.All.Messages;

namespace TauCode.Mq.Autofac.Demo.Logger.Handlers
{
    public class LoggerGreetingHandler : MessageHandlerBase<Greeting>
    {
        public override void Handle(Greeting message)
        {
            var json = JsonConvert.SerializeObject(message);
            Console.WriteLine(json);
        }
    }
}
