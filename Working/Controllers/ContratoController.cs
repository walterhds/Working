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
    public class ContratoController : Controller
    {
        private readonly ContratoService _contratoService;
        private readonly ClienteService _clienteService;

        public ContratoController()
        {
            _contratoService = Dependencia.Resolver<ContratoService>();
            _clienteService = Dependencia.Resolver<ClienteService>();
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
            var cliente = new Cliente();
            TryUpdateModel(contrato);
            contrato.DataRegistro = DateTime.Now;
            cliente = _clienteService.ObterPorId(Convert.ToInt16(Request["Cliente"]));
            contrato.Cliente = cliente;
            _contratoService.Cadastrar(contrato);
            return RedirectToAction("Index", "Contrato");
        }

    }
}
