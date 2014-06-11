using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Microsoft.Ajax.Utilities;
using Working.ViewsModels;

namespace Working.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly CompromissoService _compromissoService;
        private DadosIndex _dadosIndex;
        private readonly JobService _jobService;

        public HomeController()
        {
            _compromissoService = Dependencia.Resolver<CompromissoService>();
            _jobService = Dependencia.Resolver<JobService>();
        }

        public ActionResult Index()
        {
            _dadosIndex = new DadosIndex
            {
                Compromissos = _compromissoService.Listar(e => true),
                Jobs = _jobService.Listar(e => true),
            };
            return View(_dadosIndex);
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
