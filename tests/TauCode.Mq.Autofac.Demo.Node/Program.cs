using Autofac;
using System;
using TauCode.Mq.Autofac.Demo.All.Messages;
using TauCode.Mq.Autofac.Demo.Node.CommandLine;
using TauCode.Mq.Autofac.Demo.Node.Handlers;
using TauCode.Mq.EasyNetQ;

namespace TauCode.Mq.Autofac.Demo.Node
{
    internal class Program
    {
        private string _name;
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
            containerBuilder.RegisterType<NodeGreetingHandler>().AsSelf().InstancePerLifetimeScope();
            containerBuilder.RegisterType<NodeGreetingResponseHandler>().AsSelf().InstancePerLifetimeScope();
            containerBuilder.RegisterType<NodeNotificationHandler>().AsSelf().InstancePerLifetimeScope();

            containerBuilder.RegisterType<EasyNetQMessageSubscriber>().As<IMessageSubscriber>().SingleInstance();
            containerBuilder.RegisterType<EasyNetQMessagePublisher>().As<IMessagePublisher>().SingleInstance();

            containerBuilder
                .Register(context => new AutofacMessageHandlerContextFactory(context.Resolve<ILifetimeScope>()))
                .As<IMessageHandlerContextFactory>()
                .SingleInstance();

            _container = containerBuilder.Build();

        }

        public void Run()
        {
            while (true)
            {
                try
                {
                    Console.Write("Name: ");
                    _name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(_name))
                    {
                        throw new Exception("Bad name.");
                    }

                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            Console.WriteLine($"Node started with name '{_name}'.");

            //_publisher = new EasyNetQMessagePublisherLab
            //{
            //    Name = _name,
            //    ConnectionString = "host=localhost",
            //};

            _publisher = _container.Resolve<IMessagePublisher>();
            ((EasyNetQMessagePublisher)_publisher).ConnectionString = "host=localhost";

            _subscriber = _container.Resolve<IMessageSubscriber>();
            _subscriber.Name = _name;
            ((EasyNetQMessageSubscriber)_subscriber).ConnectionString = "host=localhost";


            _publisher.Start();

            _subscriber.Subscribe(typeof(NodeGreetingHandler), _name);
            _subscriber.Subscribe(typeof(NodeGreetingResponseHandler), _name);
            _subscriber.Subscribe(typeof(NodeNotificationHandler), _name);

            _subscriber.Start();

            var parser = new CliParser();


            while (true)
            {
                Console.Write($"{_name} >");
                var txt = Console.ReadLine();
                if (txt == "exit")
                {
                    break;
                }

                var command = parser.Parse(txt);
                _publisher.Publish(new Greeting(_name, command.To, command.MessageText), command.To);
            }

            _subscriber.Dispose();
            _publisher.Dispose();
        }
    }
}
