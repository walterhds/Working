using Castle.Windsor;
using Castle.MicroKernel.Registration;
using NHibernate;
using Percistencia.Ado;
using Dominio.Servicos;
using Dominio.Repositorios;
using Percistencia.Ado.Repositorio;

namespace Dependencias
{
    public static class Dependencia
    {
        private static bool _configurado = false;
        private static WindsorContainer _container;

        public static void Configurar()
        {
            if (_configurado)
            {
                return;
            }

            _container = new WindsorContainer();

            //Sessão
            _container.Register(Component.For<ISession>().Instance(NHibernateHelper.AbrirSessao()));

            //Services
            _container.Register(Component.For<CargoService>().ImplementedBy<CargoService>());
            _container.Register(Component.For<FuncionarioService>().ImplementedBy<FuncionarioService>());
            _container.Register(Component.For<ClienteService>().ImplementedBy<ClienteService>());
            _container.Register(Component.For<CompromissoService>().ImplementedBy<CompromissoService>());
            _container.Register(Component.For<JobService>().ImplementedBy<JobService>());

            //Repositorios
            _container.Register(Component.For<ICargoRepositorio>().ImplementedBy<CargoRepositorio>());
            _container.Register(Component.For<IFuncionarioRepositorio>().ImplementedBy<FuncionarioRepositorio>());
            _container.Register(Component.For<IClienteRepositorio>().ImplementedBy<ClienteRepositorio>());
            _container.Register(Component.For<ICompromissoRepositorio>().ImplementedBy<CompromissoRepositorio>());
            _container.Register(Component.For<IJobRepositorio>().ImplementedBy<JobRepositorio>());
        }

        public static T Resolver<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
