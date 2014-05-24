using System.Web.Mvc;
using Dependencias;
using Dominio.Servicos;

namespace Working.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly CompromissoService _compromissoService;

        public HomeController()
        {
            _compromissoService = Dependencia.Resolver<CompromissoService>();
        }

        public ActionResult Index()
        {
            var lista = _compromissoService.Listar(e => true);
            return View(lista);
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
