using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Microsoft.Ajax.Utilities;
using Rotativa;
using Working.Models;

namespace Working.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _clienteService;
        private readonly FuncionarioService _funcionarioService;

        public ClienteController()
        {
            _clienteService = Dependencia.Resolver<ClienteService>();
            _funcionarioService = Dependencia.Resolver<FuncionarioService>();
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

        public ActionResult RelatorioClientes()
        {
            var pdf = new ViewAsPdf(_clienteService.Listar(e => true));
            return pdf;
        }

        public JsonResult VerificarCadastroLoginCliente(string id)
        {
            var listaFuncionario = _funcionarioService.Listar(e => true);
            var listaClientes = _clienteService.Listar(e => true);
            if (listaFuncionario.Any(e => e.Login == id) || listaClientes.Any(e => e.Login == id))
            {
                return Json("existente", JsonRequestBehavior.AllowGet);
            }
            return Json("valido", JsonRequestBehavior.AllowGet);
        }
    }
}
