using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Servicos;
using Working.Models;

namespace Working.Controllers
{
    public class ParcelasPagarController : Controller
    {
        private readonly ParcelasPagarService _parcelasPagarService;
        private readonly ContasPagarService _contasPagarService;

        public ParcelasPagarController()
        {
            _parcelasPagarService = Dependencia.Resolver<ParcelasPagarService>();
            _contasPagarService = Dependencia.Resolver<ContasPagarService>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarParcelasContaPagar(int id)
        {
            var conta = _contasPagarService.ObterPorId(id);
            var parcelas = _parcelasPagarService.Listar(e => e.Conta == conta);
            var lista = parcelas.Select(i => new ObjetoParcela
            {
                Id = i.Id,
                DataVencimento = i.DataVencimento.Day.ToString("D2") + "/" + i.DataVencimento.ToString("MMM"),
                Valor = i.Valor,
                Situacao = i.Situacao,
                NumeroParcela = i.NumeroParcela,
                DataRegistro = i.DataRegistro
            })
                .Where(e => e.Situacao != "Cancelada")
                .OrderByDescending(e => e.NumeroParcela)
                .ThenByDescending(e => e.DataRegistro)
                .ToList();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public void Pagar(int id, string valor)
        {
            var decimalValor = Convert.ToDouble(valor);
            var parcela = _parcelasPagarService.ObterPorId(id);
            parcela.Situacao = "Paga";
            parcela.ValorPago = Convert.ToDecimal(decimalValor);
            parcela.DataRegistro = DateTime.Now;
            _parcelasPagarService.Cadastrar(parcela);
        }

    }
}
