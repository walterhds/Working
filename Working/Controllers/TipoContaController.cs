using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
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
            return View();
        }



    }
}
