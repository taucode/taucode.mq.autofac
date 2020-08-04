using Autofac;
using Serilog;
using System;
using TauCode.Cli;
using TauCode.Cli.Exceptions;
using TauCode.Mq.Autofac.Demo.Node.Cli;
using TauCode.Mq.Autofac.Demo.Node.Handlers;
using TauCode.Mq.EasyNetQ;

namespace TauCode.Mq.Autofac.Demo.Node
{
    internal class Program
    {
        private class ExitException : Exception
        {

        }

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
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.Console()
                .CreateLogger();

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

            //var parser = new CliParser();
            //while (true)
            //{
            //    Console.Write($"{_name} >");
            //    var txt = Console.ReadLine();
            //    if (txt == "exit")
            //    {
            //        break;
            //    }

            //    var command = parser.Parse(txt);
            //    _publisher.Publish(new Greeting(_name, command.To, command.MessageText), command.To);
            //}

            var host = new Host
            {
                Output = Console.Out,
                UserName = _name,
                Publisher = _publisher,
            };

            host.AddCustomHandler(
                () => throw new ExitException(),
                "exit");

            host.AddCustomHandler(
                Console.Clear,
                "cls");

            while (true)
            {
                try
                {
                    Console.Write(" : ");
                    var line = Console.ReadLine();
                    var command = host.ParseLine(line);
                    host.DispatchCommand(command);
                }
                catch (CliCustomHandlerException)
                {
                    // ignore
                }
                catch (ExitException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }

            _subscriber.Dispose();
            _publisher.Dispose();
        }
    }
}
