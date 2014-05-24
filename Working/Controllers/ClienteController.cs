using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Microsoft.Ajax.Utilities;

namespace Working.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;

        public ClienteController()
        {
            _clienteService = Dependencia.Resolver<ClienteService>();
        }

        public ActionResult Index()
        {
            var lista = _clienteService.Listar(e => true);
            return View(lista);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(FormCollection form)
        {
            var cliente = new Cliente();
            TryUpdateModel(cliente);
            cliente.DataRegistro = DateTime.Now;
            cliente.Permissao = 0;
            _clienteService.Cadastrar(cliente);
            return RedirectToAction("Index");
        }
    }
}
