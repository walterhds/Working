﻿using Castle.Windsor;
using Castle.MicroKernel.Registration;
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

            //Services
            _container.Register(Component.For<CargoService>().ImplementedBy<CargoService>());
            _container.Register(Component.For<FuncionarioService>().ImplementedBy<FuncionarioService>());
            _container.Register(Component.For<ClienteService>().ImplementedBy<ClienteService>());
            _container.Register(Component.For<CompromissoService>().ImplementedBy<CompromissoService>());
            _container.Register(Component.For<JobService>().ImplementedBy<JobService>());
            _container.Register(Component.For<PecaService>().ImplementedBy<PecaService>());
            _container.Register(Component.For<FornecedorService>().ImplementedBy<FornecedorService>());
            _container.Register(Component.For<SituacaoJobService>().ImplementedBy<SituacaoJobService>());
            _container.Register(Component.For<TimelineJobService>().ImplementedBy<TimelineJobService>());
            _container.Register(Component.For<MuralService>().ImplementedBy<MuralService>());
            _container.Register(Component.For<FuncionalidadeService>().ImplementedBy<FuncionalidadeService>());
            _container.Register(Component.For<AcessoService>().ImplementedBy<AcessoService>());
            _container.Register(Component.For<ContratoService>().ImplementedBy<ContratoService>());
            _container.Register(Component.For<TipoContaService>().ImplementedBy<TipoContaService>());
            _container.Register(Component.For<ParcelasReceberService>().ImplementedBy<ParcelasReceberService>());
            _container.Register(Component.For<AlteracaoContratoService>().ImplementedBy<AlteracaoContratoService>());
            _container.Register(Component.For<ContasPagarService>().ImplementedBy<ContasPagarService>());
            _container.Register(Component.For<ParcelasPagarService>().ImplementedBy<ParcelasPagarService>());

            //Repositorios
            _container.Register(Component.For<ICargoRepositorio>().ImplementedBy<CargoRepositorio>());
            _container.Register(Component.For<IFuncionarioRepositorio>().ImplementedBy<FuncionarioRepositorio>());
            _container.Register(Component.For<IClienteRepositorio>().ImplementedBy<ClienteRepositorio>());
            _container.Register(Component.For<ICompromissoRepositorio>().ImplementedBy<CompromissoRepositorio>());
            _container.Register(Component.For<IJobRepositorio>().ImplementedBy<JobRepositorio>());
            _container.Register(Component.For<IPecaRepositorio>().ImplementedBy<PecaRepositorio>());
            _container.Register(Component.For<IFornecedorRepositorio>().ImplementedBy<FornecedorRepositorio>());
            _container.Register(Component.For<ISituacaoJobRepositorio>().ImplementedBy<SituacaoJobRepositorio>());
            _container.Register(Component.For<ITimelineJobRepositorio>().ImplementedBy<TimelineJobRepositorio>());
            _container.Register(Component.For<IMuralRepositorio>().ImplementedBy<MuralRepositorio>());
            _container.Register(Component.For<IFuncionalidadeRepositorio>().ImplementedBy<FuncionalidadeRepositorio>());
            _container.Register(Component.For<IAcessoRepositorio>().ImplementedBy<AcessoRepositorio>());
            _container.Register(Component.For<IContratoRepositorio>().ImplementedBy<ContratoRepositorio>());
            _container.Register(Component.For<ITipoContaRepositorio>().ImplementedBy<TipoContaRepositorio>());
            _container.Register(Component.For<IParcelasReceberRepositorio>().ImplementedBy<ParcelasReceberRepositorio>());
            _container.Register(
                Component.For<IAlteracaoContratoRepositorio>().ImplementedBy<AlteracaoContratoRepositorio>());
            _container.Register(Component.For<IContasPagarRepositorio>().ImplementedBy<ContasPagarRepositorio>());
            _container.Register(Component.For<IParcelasPagarRepositorio>().ImplementedBy<ParcelasPagarRepositorio>());
        }

        public static T Resolver<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
