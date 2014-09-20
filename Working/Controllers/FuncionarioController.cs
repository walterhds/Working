using System.Collections.Generic;
using System.Linq;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using System;
using System.Web.Mvc;
using Percistencia.Ado;
using Working.Models;
using Working.ViewsModels;

namespace Working.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly FuncionarioService _funcionarioService;
        private readonly CargoService _cargoService;
        private DadosFuncionario _dadosFuncionario;

        public FuncionarioController()
        {
            _funcionarioService = Dependencia.Resolver<FuncionarioService>();
            _cargoService = Dependencia.Resolver<CargoService>();
        }

        public ActionResult Index()
        {
            var lista = _funcionarioService.Listar(e => true);
            return View(lista);
        }

        public ActionResult Cadastrar()
        {
            var lista = _cargoService.Listar(e => true);
            return View(lista);
        }

        [HttpPost]
        public ActionResult Cadastrar(FormCollection form)
        {
            var funcionario = new Funcionario();
            var cargo = new Cargo();
            TryUpdateModel(funcionario);
            funcionario.DataRegistro = DateTime.Now;
            cargo = _cargoService.ObterPorId(Convert.ToInt16(Request["Cargo"]));
            funcionario.Cargo = cargo;
            _funcionarioService.Cadastrar(funcionario);
            return RedirectToAction("Index");
        }

        public ActionResult Alterar(int id)
        {
            _dadosFuncionario = new DadosFuncionario
            {
                Funcionario = _funcionarioService.ObterPorId(id),
                ListaCargo = _cargoService.Listar(e => true),
            };
            return View(_dadosFuncionario);
        }

        [HttpPost]
        public ActionResult Alterar(FormCollection form, int id)
        {
            var funcionario = _funcionarioService.ObterPorId(id);
            var cargo = new Cargo();
            funcionario.DataRegistro = DateTime.Now;
            TryUpdateModel(funcionario);
            cargo = _cargoService.ObterPorId(Convert.ToInt16(Request["Cargo"]));
            funcionario.Cargo = cargo;
            _funcionarioService.Cadastrar(funcionario);
            return RedirectToAction("Index");
        }

        public ActionResult ListarDesligados()
        {
            var lista = _funcionarioService.Listar(e => true);
            return View(lista);
        }

        public ActionResult TornarAtivo(int id)
        {
            var funcionario = _funcionarioService.ObterPorId(id);
            funcionario.Situacao = Situacao.Ativo;
            funcionario.DataRegistro = DateTime.Now;
            TryUpdateModel(funcionario);
            _funcionarioService.Cadastrar(funcionario);
            return RedirectToAction("Index");
        }

        public JsonResult AlterarSituacao(int id, string situacao)
        {
            var funcionario = _funcionarioService.ObterPorId(id);
            var sit = SituacaoHelper.GetValueFromDescription<Situacao>(situacao);
            funcionario.Situacao = sit;
            _funcionarioService.Cadastrar(funcionario);
            return null;
        }

        public JsonResult ListarFuncionarioJson()
        {
            var funcionario = _funcionarioService.Listar(e => e.Cargo.Nome == "Designer");
            var lista = new List<Objeto>();
            foreach (var i in funcionario)
            {
                if (i.Situacao != Situacao.Desligado)
                {
                    lista.Add(new Objeto
                    {
                        id = i.Id,
                        nome = i.Nome
                    });
                }
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VerificarCadastroLogin(string id)
        {
            var listaFuncionario = _funcionarioService.Listar(e => true);
            if (listaFuncionario.Any(e => e.Login == id))
            {
                return Json("existente",JsonRequestBehavior.AllowGet);
            }
            return Json("valido",JsonRequestBehavior.AllowGet);
        }
    }
}
