using Autofac;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using Inflector;
using NHibernate;
using NHibernate.Cfg;
using System;
using System.Data.SQLite;
using System.Reflection;
using TauCode.Extensions;

namespace TauCode.Lab.Mq.NHibernate.Tests.App
{
    public static class AppHelper
    {
        public static ISessionFactory BuildSessionFactory(
            Configuration configuration,
            Assembly mappingsAssembly)
        {
            return Fluently.Configure(configuration)
                .Mappings(m => m.FluentMappings.AddFromAssembly(mappingsAssembly)
                    .Conventions.Add(ForeignKey.Format((p, t) =>
                    {
                        if (p == null) return t.Name.Underscore() + "_id";

                        return p.Name.Underscore() + "_id";
                    }))
                    .Conventions.Add(LazyLoad.Never())
                    .Conventions.Add(Table.Is(x => x.TableName.Underscore().ToUpper()))
                    .Conventions.Add(ConventionBuilder.Property.Always(x => x.Column(x.Property.Name.Underscore())))
                )
                .BuildSessionFactory();
        }

        public static ContainerBuilder AddNHibernate(
            this ContainerBuilder containerBuilder,
            Configuration configuration,
            Assembly mappingsAssembly)
        {
            containerBuilder
                .Register(c => BuildSessionFactory(configuration, mappingsAssembly))
                .As<ISessionFactory>()
                .SingleInstance();

            containerBuilder
                .Register(c =>
                {
                    var session = c.Resolve<ISessionFactory>().OpenSession();
                    ((SQLiteConnection)session.Connection).BoostSQLiteInsertions();
                    return session;
                })
                .As<ISession>()
                .InstancePerLifetimeScope();

            return containerBuilder;
        }

        public static Tuple<string, string> CreateSQLiteDatabase()
        {
            var tempDbFilePath = FileExtensions.CreateTempFilePath("zunit", ".sqlite");
            SQLiteConnection.CreateFile(tempDbFilePath);

            var connectionString = $"Data Source={tempDbFilePath};Version=3;";

            return Tuple.Create(tempDbFilePath, connectionString);
        }

        public static void BoostSQLiteInsertions(this SQLiteConnection sqLiteConnection)
        {
            if (sqLiteConnection == null)
            {
                throw new ArgumentNullException(nameof(sqLiteConnection));
            }

            using var command = sqLiteConnection.CreateCommand();
            command.CommandText = "PRAGMA journal_mode = WAL";
            command.ExecuteNonQuery();

            command.CommandText = "PRAGMA synchronous = NORMAL";
            command.ExecuteNonQuery();
        }
    }
}

