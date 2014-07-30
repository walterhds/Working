using System;
using System.Configuration;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Context;
using NHibernate.Mapping.ByCode;
using Configuration = NHibernate.Cfg.Configuration;

namespace Percistencia.Ado
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    _sessionFactory = BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession GetCurrentSession()
        {
            if (!CurrentSessionContext.HasBind(SessionFactory))
            {
                CurrentSessionContext.Bind(SessionFactory.OpenSession());
            }

            return SessionFactory.GetCurrentSession();
        }

        public static void DisposeSession()
        {
            var session = GetCurrentSession();
            session.Close();
            session.Dispose();
        }

        public static ISessionFactory BuildSessionFactory()
        {
            //_connection = "data source=(local)\\DBWORKFLOW; Initial Catalog=DBworkflow; User Id=sa; Password=3751861";

            var mapper = new ModelMapper();

            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            HbmMapping mapeamento = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();

            string databaseType = ConfigurationManager.AppSettings["DataBaseType"];

            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<NHibernate.Dialect.MsSql2000Dialect>();
                c.ConnectionStringName = "Default";
            });
            
            configuration.AddMapping(mapeamento);
            configuration.CurrentSessionContext<WebSessionContext>();
            return configuration.BuildSessionFactory();
        }

        public static void BeginTransaction()
        {
            GetCurrentSession().BeginTransaction();
        }

        public static void CommitTransaction()
        {
            var session = GetCurrentSession();
            if (session.Transaction.IsActive)
            {
                session.Transaction.Commit();
            }
        }

        public static void RollbackTransaction()
        {
            var session = GetCurrentSession();
            if (session.Transaction.IsActive)
            {
                session.Transaction.Rollback();
            }
        }
    }
}
