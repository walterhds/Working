using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace Percistencia.Ado
{
    public static class NHibernateHelper
    {
        private static string _connection;
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    IniciarSessao();
                }
                return _sessionFactory;
            }
        }

        public static void IniciarSessao()
        {
            _connection = "data source=(local)\\DBWORKFLOW; Initial Catalog=DBworkflow; User Id=sa; Password=3751861";

            var mapper = new ModelMapper();

            mapper.AddMappings(Assembly.GetExecutingAssembly().GetExportedTypes());

            HbmMapping mapeamento = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();

            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<NHibernate.Dialect.MsSql2008Dialect>();
                c.ConnectionString = _connection;
            });

            configuration.AddMapping(mapeamento);

            _sessionFactory = configuration.BuildSessionFactory();
        }

        public static ISession AbrirSessao()
        {
            return SessionFactory.OpenSession();
        }
    }
}
