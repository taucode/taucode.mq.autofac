using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate.Cfg;

namespace TauCode.Lab.Mq.NHibernate.Tests.App
{
    public class Startup : IAppStartup
    {
        private readonly string _tempFilePath;
        private readonly string _connectionString;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var tuple = AppHelper.CreateSQLiteDatabase();
            _tempFilePath = tuple.Item1;
            _connectionString = tuple.Item2;
        }

        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var cqrsAssembly = typeof(CoreBeacon).Assembly;
            services
                .AddControllers()
                .AddApplicationPart(typeof(Startup).Assembly)
                .AddNewtonsoftJson(options => options.UseCamelCasing(false));
        }

        private Configuration CreateConfiguration()
        {
            var configuration = new Configuration();
            configuration.Properties.Add("connection.connection_string", _connectionString);
            configuration.Properties.Add("connection.driver_class", "NHibernate.Driver.SQLite20Driver");
            configuration.Properties.Add("connection.provider", "NHibernate.Connection.DriverConnectionProvider");
            configuration.Properties.Add("dialect", "NHibernate.Dialect.SQLiteDialect");

            return configuration;
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            var configuration = this.CreateConfiguration();
            containerBuilder.AddNHibernate(configuration, this.GetType().Assembly);

            containerBuilder
                .RegisterInstance(this)
                .As<IAppStartup>()
                .SingleInstance();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            this.AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
