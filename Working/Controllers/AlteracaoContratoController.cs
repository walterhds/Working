using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;

namespace Working.Controllers
{
    public class AlteracaoContratoController : Controller
    {
        private readonly AlteracaoContratoService _alteracaoContratoService;
        private readonly ContratoService _contratoService;
        private readonly ParcelasReceberService _parcelasReceberService;

        public AlteracaoContratoController()
        {
            _alteracaoContratoService = Dependencia.Resolver<AlteracaoContratoService>();
            _contratoService = Dependencia.Resolver<ContratoService>();
            _parcelasReceberService = Dependencia.Resolver<ParcelasReceberService>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar(int id)
        {
            var contrato = _contratoService.ObterPorId(id);
            return View(contrato);
        }

        [HttpPost]
        public ActionResult Cadastrar(FormCollection form)
        {
            var alteracaoContrato = new AlteracaoContrato();
            TryUpdateModel(alteracaoContrato);
            var contrato = _contratoService.ObterPorFiltro(e => e.NumeroContrato == Convert.ToInt16(Request["Contrato"]));
            alteracaoContrato.Contrato = contrato;
            CadastrarParcelas(Convert.ToInt16(Request["NumeroParcelas"]), contrato, Convert.ToDecimal(Request["Valor"]), Convert.ToDateTime(Request["DataPrimeiraParcela"]));
            alteracaoContrato.DataRegistro = DateTime.Now;
            _alteracaoContratoService.Cadastrar(alteracaoContrato);
            return RedirectToAction("Index", "Contrato");
        }

        public void CadastrarParcelas(int numeroParcelas, Contrato contrato, decimal valor, DateTime primeiraParcela)
        {
            var listaParcelasReceberContrato = _parcelasReceberService.Listar(e => e.Contrato == contrato);
            var valorTotal = valor;
            valorTotal = listaParcelasReceberContrato.Where(e => e.Situacao == "Recebida").Aggregate(valorTotal, (current, i) => current - i.ValorRecebido);
            var centavos = valorTotal -
                               (Math.Round(valorTotal / numeroParcelas, 2) * numeroParcelas);
            var numeroParcela = listaParcelasReceberContrato.LastOrDefault().NumeroParcela;
            foreach (var i in listaParcelasReceberContrato.Where(e => e.Situacao == "Aberta"))
            {
                i.Situacao = "Cancelada";
                _parcelasReceberService.Cadastrar(i);
            }
            for (var i = 1; i <= numeroParcelas; i++)
            {
                var parcela = new ParcelasReceber
                {
                    DataVencimento = i == 1 ? primeiraParcela : primeiraParcela.AddMonths(1),
                    Valor = i != numeroParcelas ? Math.Round(valorTotal / numeroParcelas, 2) : Math.Round(valorTotal / numeroParcelas, 2) + centavos,
                    Situacao = "Aberta",
                    Contrato = contrato,
                    DataRegistro = DateTime.Now,
                    NumeroParcela = ++numeroParcela,
                    DataRecebida = Convert.ToDateTime("01/01/01")
                };
                _parcelasReceberService.Cadastrar(parcela);
            }
        }

        public JsonResult VerificarCadastroAlteracao(int id, int numeroContrato)
        {
            var listaAlteracoes = _alteracaoContratoService.Listar(e => true);
            var contrato = _contratoService.ObterPorFiltro(e => e.NumeroContrato == numeroContrato);
            return Json(listaAlteracoes.Any(e => e.NumeroAlteracao == id && e.Contrato == contrato) ? "existente" : "valido", JsonRequestBehavior.AllowGet);
        }
    }
}
