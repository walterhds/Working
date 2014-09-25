using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Remotion.Linq.Clauses;
using Working.ViewsModels;

namespace Working.Controllers
{
    public class ContasPagarController : Controller
    {
        private readonly ContasPagarService _contasPagarService;
        private readonly TipoContaService _tipoContaService;
        private DadosContasPagarIndex _dadosContasPagarIndex;
        private readonly ParcelasPagarService _parcelasPagarService;

        public ContasPagarController()
        {
            _contasPagarService = Dependencia.Resolver<ContasPagarService>();
            _tipoContaService = Dependencia.Resolver<TipoContaService>();
            _parcelasPagarService = Dependencia.Resolver<ParcelasPagarService>();
        }

        public ActionResult Index()
        {
            _dadosContasPagarIndex = new DadosContasPagarIndex
            {
                ContasPagar = _contasPagarService.Listar(e => true),
                ParcelasPagar = _parcelasPagarService.Listar(e => true)
            };
            return View(_dadosContasPagarIndex);
        }

        public ActionResult Cadastrar()
        {
            return View(_tipoContaService.Listar(e => true));
        }

        [HttpPost]
        public ActionResult Cadastrar(FormCollection form)
        {
            var conta = new ContasPagar();
            TryUpdateModel(conta);
            conta.TipoConta = _tipoContaService.ObterPorId(Convert.ToInt16(Request["TipoConta"]));
            conta.DataRegistro = DateTime.Now;
            _contasPagarService.Cadastrar(conta);
            CadastrarParcelas(conta);
            return RedirectToAction("Index", "ContasPagar");
        }

        public void CadastrarParcelas(ContasPagar contaPagar)
        {
            var conta = _contasPagarService.ObterPorFiltro(e => e.DataRegistro == contaPagar.DataRegistro);
            for (var i = 1; i <= conta.NumeroParcelas; i++)
            {
                var centavos = conta.Valor -
                               (Math.Round(conta.Valor / conta.NumeroParcelas, 2) * conta.NumeroParcelas);
                var parcela = new ParcelasPagar
                {
                    Conta = conta,
                    NumeroParcela = i,
                    DataRegistro = DateTime.Now,
                    Valor = i != conta.NumeroParcelas ? Math.Round(conta.Valor / conta.NumeroParcelas, 2) : Math.Round(conta.Valor / conta.NumeroParcelas, 2) + centavos,
                    DataVencimento =
                        i == 1 ? conta.DataPrimeiraParcela : conta.DataPrimeiraParcela.AddMonths(i - 1),
                    Situacao = "Aberta",
                    DataPagamento = Convert.ToDateTime("01/01/01")
                };
                _parcelasPagarService.Cadastrar(parcela);
            }
        }

    }
}
