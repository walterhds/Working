using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Microsoft.Ajax.Utilities;
using Working.Models;

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

        public ActionResult Alterar(int id)
        {
            var cliente = _clienteService.ObterPorId(id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Alterar(FormCollection form, int id)
        {
            var cliente = _clienteService.ObterPorId(id);
            cliente.DataRegistro = DateTime.Now;
            TryUpdateModel(cliente);
            _clienteService.Cadastrar(cliente);

            return RedirectToAction("Index");
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

        public JsonResult ListarClienteJson()
        {
            var cliente = _clienteService.Listar(e => true);
            var lista = new List<Objeto>();
            foreach (var i in cliente)
            {
                lista.Add(new Objeto { id = i.Id, nome = i.Nome });
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}
