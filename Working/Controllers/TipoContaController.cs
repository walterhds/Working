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
    public class TipoContaController : Controller
    {
        private readonly TipoContaService _tipoContaService;

        public TipoContaController()
        {
            _tipoContaService = Dependencia.Resolver<TipoContaService>();
        }

        public ActionResult Index()
        {
            var lista = _tipoContaService.Listar(e => true);
            return View(lista);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(FormCollection form)
        {
            var tipoConta = new TipoConta();
            TryUpdateModel(tipoConta);
            tipoConta.DataRegistro = DateTime.Now;
            _tipoContaService.Cadastrar(tipoConta);
            return RedirectToAction("Index");
        }

    }
}
