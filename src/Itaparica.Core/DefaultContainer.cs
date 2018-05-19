using System;
using System.Reflection;
using System.Web;
using Castle.Core;
using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Itaparica.Core.Domain.Repositories;
using Itaparica.Core.Domain.Services;
using Itaparica.Core.Infra.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace Itaparica.Core
{
    public class DefaultContainer : WindsorContainer
    {
        /// <summary>
        /// Configura container para contexto de aplicação asp.net
        /// </summary>
        public void SetupForWeb()
        {
            if (HttpContext.Current == null)
                throw new Exception("Esta configuração é apenas para web.");

            AddFacility<StartableFacility>();
            RegisterPersistence(LifestyleType.PerWebRequest, BuildDatabaseConfiguration());
            RegisterRepositories();
            RegisterServices();
        }

        /// <summary>
        /// Configuração para Testes unitarios ou integração. Base de dados vazia.
        /// </summary>
        public void SetupForTests()
        {
            if (HttpContext.Current != null)
                throw new Exception("Esta configuração é apenas para Testes.");

            RegisterPersistence(LifestyleType.Thread, BuildDatabaseConfiguration());

            RegisterRepositories();
            RegisterServices();
        }

        private void RegisterPersistence(LifestyleType lifestyle, Configuration config)
        {
            Register(
                Component.For<ISessionFactory>().UsingFactoryMethod(delegate { return config.BuildSessionFactory(); }),
                Component.For<ISession>().UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession(), false).LifeStyle.Is(lifestyle),
                Component.For<Configuration>().Instance(config));
        }

        private void RegisterRepositories()
        {
            Register(Component.For<TransactionInterceptor>().LifeStyle.Transient);

            Register(Classes.FromAssemblyContaining<DefaultContainer>()
                .BasedOn(typeof(IRepository))
                .WithService.AllInterfaces()
                .Configure(x => x.Interceptors(typeof(TransactionInterceptor)))
                .Configure(c => c.LifestyleTransient()));
        }

        private void RegisterServices()
        {
            Register(Classes.FromAssemblyContaining<DefaultContainer>()
                .BasedOn(typeof(IService))
                .WithService.AllInterfaces()
                .Configure(x => x.Interceptors(typeof(TransactionInterceptor)))
                .Configure(c => c.LifestyleTransient()));
        }

        private static Configuration BuildDatabaseConfiguration()
        {
            var config = new Configuration();

            config.DataBaseIntegration(x =>
            {
                x.Driver<SQLite20Driver>();
                x.Dialect<SQLiteDialect>();
                x.ConnectionProvider<DriverConnectionProvider>();
                x.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                x.ConnectionString = $"Data Source=|DataDirectory|\\winterfell.db;Version=3;New=True;";
                x.Timeout = 255;
                x.BatchSize = 100;
                x.LogFormattedSql = true;
                x.LogSqlInConsole = true;
                x.AutoCommentSql = false;
            });

            var mapper = new ModelMapper();

            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            var domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            if (domainMapping.Items != null)
                config.AddMapping(domainMapping);

            return config;
        }
}
}