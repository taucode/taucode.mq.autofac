using Autofac;
using EasyNetQ;
using EasyNetQ.NonGeneric;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TauCode.Extensions;
using TauCode.Lab.Mq.NHibernate.Tests.App;
using TauCode.Lab.Mq.NHibernate.Tests.App.Client;
using TauCode.Lab.Mq.NHibernate.Tests.App.Client.Messages.Notes;

namespace TauCode.Lab.Mq.NHibernate.Tests.Integration
{
    [TestFixture]
    public class NewNoteMessageHandlerTest
    {
        private TestFactory _factory;
        private ILifetimeScope _container;
        private IAppClient _appClient;

        [SetUp]
        public void SetUp()
        {
            Inflector.Inflector.SetDefaultCultureFunc = () => new CultureInfo("en-US");
            _factory = new TestFactory();
            var httpClient = this.CreateHttpClient();
            _appClient = new AppClient(httpClient);
        }

        [TearDown]
        public void TearDown()
        {
            _container.Dispose();
        }

        private HttpClient CreateHttpClient()
        {
            var httpClient = _factory
                .WithWebHostBuilder(builder => builder.UseSolutionRelativeContentRoot(@"test-lab\TauCode.Lab.Mq.NHibernate.Tests.Integration"))
                .CreateClient();

            var testServer = _factory.Factories.Single().Server;

            var startup = (Startup)testServer.Services.GetService<IAppStartup>();
            _container = startup.AutofacContainer;

            return httpClient;
        }


        [Test]
        public async Task Publish_ValidNote_CanGetNote()
        {
            // Arrange
            using var bus = RabbitHutch.CreateBus("host=localhost");
            var message = new NewNoteMessage
            {
                CorrelationId = new Guid("3794470c-c02c-40af-921d-b9a2730160c0").ToString(),
                CreatedAt = "2020-01-01Z".ToUtcDayOffset().AddHours(1.5),
                UserId = "ak@m.net",
                Subject = "Ocean",
                Body = "Ready for.",
                Importance = ImportanceDto.Medium,
            };

            // Act
            bus.Publish(typeof(NewNoteMessage), message);

            await Task.Delay(300);

            var notes = await _appClient.GetUserNotes("ak@m.net");

            // Assert
            throw new NotImplementedException();
        }
    }
}
