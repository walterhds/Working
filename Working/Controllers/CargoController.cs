using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using System;
using System.Web.Mvc;

namespace Working.Controllers
{
    public class CargoController : Controller
    {
        private readonly CargoService _cargoService;

        public CargoController()
        {
            _cargoService = Dependencia.Resolver<CargoService>();
        }

        public ActionResult Index()
        {
            var lista = _cargoService.Listar(e => true);
            return View(lista);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(FormCollection form)
        {
            var cargo = new Cargo();
            TryUpdateModel(cargo);
            cargo.DataRegistro = DateTime.Now;
            _cargoService.Cadastrar(cargo);
            return RedirectToAction("Index");
        }

        public ActionResult Remover(int id)
        {
            var cargo = _cargoService.ObterPorId(id);
            return View(cargo);
        }

        [HttpPost]
        public ActionResult Remover(FormCollection form, int id)
        {
            var cargo = _cargoService.ObterPorId(id);
            _cargoService.Remover(cargo);
            return RedirectToAction("Index");
        }

        public ActionResult Alterar(int id)
        {
            var cargo = _cargoService.ObterPorId(id);
            return View(cargo);
        }

        [HttpPost]
        public ActionResult Alterar(FormCollection form, int id)
        {
            var cargo = _cargoService.ObterPorId(id);
            cargo.DataRegistro = DateTime.Now;
            TryUpdateModel(cargo);
            _cargoService.Cadastrar(cargo);
            return RedirectToAction("Index");
        }
    }
}
