using Autofac;
using Serilog;
using System;
using TauCode.Mq.Autofac.Demo.Logger.Handlers;
using TauCode.Mq.EasyNetQ;

namespace TauCode.Mq.Autofac.Demo.Logger
{
    internal class Program
    {
        private IMessagePublisher _publisher;
        private IMessageSubscriber _subscriber;

        private readonly IContainer _container;

        private static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }

        public Program()
        {
            var containerBuilder = new ContainerBuilder();

            // todo: register them at once.
            containerBuilder.RegisterType<LoggerGreetingHandler>().AsSelf().InstancePerLifetimeScope();
            containerBuilder.RegisterType<LoggerGreetingResponseHandler>().AsSelf().InstancePerLifetimeScope();
            containerBuilder.RegisterType<LoggerNotificationHandler>().AsSelf().InstancePerLifetimeScope();

            containerBuilder.RegisterType<EasyNetQMessageSubscriber>().As<IMessageSubscriber>().SingleInstance();

            containerBuilder
                .Register(context => new AutofacMessageHandlerContextFactory(context.Resolve<ILifetimeScope>()))
                .As<IMessageHandlerContextFactory>()
                .SingleInstance();


            _container = containerBuilder.Build();
        }

        public void Run()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.Console()
                .CreateLogger();

            const string name = "logger";

            Console.WriteLine($"App '{name}' started.");

            // todo: function never returns (thanks resharper)
            while (true)
            {
                _publisher = new EasyNetQMessagePublisher
                {
                    Name = name,
                    ConnectionString = "host=localhost",
                };

                _subscriber = _container.Resolve<IMessageSubscriber>();
                _subscriber.Name = name;
                ((EasyNetQMessageSubscriber)_subscriber).ConnectionString = "host=localhost";

                _publisher.Start();

                _subscriber.Subscribe(typeof(LoggerGreetingHandler));
                _subscriber.Subscribe(typeof(LoggerGreetingResponseHandler));
                _subscriber.Subscribe(typeof(LoggerNotificationHandler));

                _subscriber.Start();

                while (true)
                {
                    Console.Write($"{name} >");
                    var cmd = Console.ReadLine();
                    if (cmd == "exit")
                    {
                        break;
                    }
                }

                _subscriber.Dispose();
                _publisher.Dispose();
            }
        }
    }
}
