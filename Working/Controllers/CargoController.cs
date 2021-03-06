﻿using System.Collections.Generic;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using System;
using System.Web.Mvc;
using NHibernate.Mapping;

namespace Working.Controllers
{
    public class CargoController : Controller
    {
        private readonly CargoService _cargoService;
        private readonly AcessoService _acessoService;

        public CargoController()
        {
            _cargoService = Dependencia.Resolver<CargoService>();
            _acessoService = Dependencia.Resolver<AcessoService>();
        }

        public ActionResult Index()
        {
            var lista = _cargoService.Listar(e => true);
            return View(lista);
        }

        public ActionResult Cadastrar()
        {
            var lista = _acessoService.Listar(e => true);
            return View(lista);
        }

        [HttpPost]
        public ActionResult Cadastrar(FormCollection form)
        {
            var cargo = new Cargo();
            TryUpdateModel(cargo);
            cargo.DataRegistro = DateTime.Now;
            var acessos = Request["Acesso"].Split(',');
            IList<Acesso> lista = new List<Acesso>();
            foreach (var i in acessos)
            {
                lista.Add(_acessoService.ObterPorId(Convert.ToInt16(i)));
            }
            cargo.Acessos = lista;
            _cargoService.Cadastrar(cargo);
            return RedirectToAction("Index","Home");
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
