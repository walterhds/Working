using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Working.Models;

namespace Working.Controllers
{
    public class ContratoController : Controller
    {
        private readonly ContratoService _contratoService;
        private readonly ClienteService _clienteService;
        private readonly ParcelasReceberService _parcelasReceberService;
        private readonly JobService _jobService;

        public ContratoController()
        {
            _contratoService = Dependencia.Resolver<ContratoService>();
            _clienteService = Dependencia.Resolver<ClienteService>();
            _parcelasReceberService = Dependencia.Resolver<ParcelasReceberService>();
            _jobService = Dependencia.Resolver<JobService>();
        }

        public ActionResult Index()
        {
            return View(_contratoService.Listar(e => true));
        }

        public ActionResult Cadastrar()
        {
            return View(_clienteService.Listar(e => true));
        }

        [HttpPost]
        public ActionResult Cadastrar(FormCollection form)
        {
            var contrato = new Contrato();
            TryUpdateModel(contrato);
            contrato.DataRegistro = DateTime.Now;
            var cliente = _clienteService.ObterPorId(Convert.ToInt16(Request["Cliente"]));
            contrato.Cliente = cliente;
            _contratoService.Cadastrar(contrato);
            CadastrarParcelas(contrato.NumeroContrato);
            return RedirectToAction("Index", "Contrato");
        }

        public void CadastrarParcelas(int numeroContrato)
        {
            var contrato = _contratoService.ObterPorFiltro(e => e.NumeroContrato == numeroContrato);
            for (var i = 1; i <= contrato.NumeroParcelas; i++)
            {
                var centavos = contrato.Valor -
                               (Math.Round(contrato.Valor / contrato.NumeroParcelas,2) * contrato.NumeroParcelas);
                var parcela = new ParcelasReceber
                {
                    Contrato = contrato,
                    NumeroParcela = i,
                    DataRegistro = DateTime.Now,
                    Valor = i != contrato.NumeroParcelas ? Math.Round(contrato.Valor / contrato.NumeroParcelas, 2) : Math.Round(contrato.Valor / contrato.NumeroParcelas, 2) + centavos,
                    DataVencimento =
                        i == 1 ? contrato.DataPrimeiraParcela : contrato.DataPrimeiraParcela.AddMonths(i - 1),
                    Situacao = "Aberta"
                };
                _parcelasReceberService.Cadastrar(parcela);
            }
        }

        public JsonResult ListarJobsContrato(int id)
        {
            var jobs = _jobService.Listar(e => true);
            var jobsContrato = jobs.Where(e => e.Contrato == _contratoService.ObterPorId(id)).ToList();
            var lista = jobsContrato.Select(i => new Objeto
            {
                id = i.Id,
                nome = i.Nome,
                DataEstimativa = i.DataEstimativa.Day.ToString("D2") + "/" + i.DataEstimativa.ToString("MMM"),
                Situacao = i.Situacao,
                Fase = i.Fase
            }).ToList();

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public void CadastrarJobsContrato(string jobsid, int id)
        {
            var jobs = jobsid.Split(',');
            var contrato = _contratoService.ObterPorId(id);
            var listaJobs = _jobService.Listar(e => true);

            foreach (var i in jobs)
            {
                var job = listaJobs.FirstOrDefault(e => e.Id == Convert.ToInt16(i));
                job.Contrato = contrato;
                _jobService.Cadastrar(job);
            }
        }

        public JsonResult VerificarCadastroContrato(int id)
        {
            var listaContratos = _contratoService.Listar(e => true);
            if (listaContratos.Any(e => e.NumeroContrato == id))
            {
                return Json("existente", JsonRequestBehavior.AllowGet);
            }
            return Json("valido", JsonRequestBehavior.AllowGet);
        }

    }
}
