using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using TauCode.Lab.Mq.NHibernate.Tests.App;

namespace TauCode.Lab.Mq.NHibernate.Tests.Integration
{
    public class TestFactory : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
