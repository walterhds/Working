using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using System;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Percistencia.Ado;
using Rotativa;
using Working.Models;
using Working.ViewsModels;

namespace Working.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly FuncionarioService _funcionarioService;
        private readonly CargoService _cargoService;
        private DadosFuncionario _dadosFuncionario;
        private readonly JobService _jobService;
        private readonly ClienteService _clienteService;

        public FuncionarioController()
        {
            _funcionarioService = Dependencia.Resolver<FuncionarioService>();
            _cargoService = Dependencia.Resolver<CargoService>();
            _jobService = Dependencia.Resolver<JobService>();
            _clienteService = Dependencia.Resolver<ClienteService>();
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

        public ActionResult Disponibilidade()
        {
            return View();
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

        public ActionResult AlterarPerfil()
        {
            var funcionario =
                _funcionarioService.ObterPorLogin((string)System.Web.HttpContext.Current.Session["usuario"]);
            return View(funcionario);
        }

        [HttpPost]
        public ActionResult AlterarPerfil(FormCollection form)
        {
            var funcionario =
                _funcionarioService.ObterPorLogin((string)System.Web.HttpContext.Current.Session["usuario"]);
            TryUpdateModel(funcionario);
            funcionario.DataRegistro = DateTime.Now;
            _funcionarioService.Cadastrar(funcionario);
            return RedirectToAction("Index", "Home");
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
            var listaClientes = _clienteService.Listar(e => true);
            if (listaFuncionario.Any(e => e.Login == id) || listaClientes.Any(e => e.Login == id))
            {
                return Json("existente", JsonRequestBehavior.AllowGet);
            }
            return Json("valido", JsonRequestBehavior.AllowGet);
        }

        public JsonResult DisponibilidadeJson(string primeiraData, string segundaData)
        {
            var listaFuncionarios = _funcionarioService.Listar(e => e.Cargo.Nome == "Designer");
            var listaJobs = _jobService.Listar(e => e.Situacao.Descricao != "Entregue" && e.Situacao.Descricao != null);
            var lista = new List<ObjetoFuncionarioDisponibilidade>();
            foreach (var i in listaFuncionarios)
            {
                var quantidadeDias = 0.0;
                var hrsPorDia = 0.0;
                var horasPeriodo = 0.0;
                var horasFuncionarioPeriodo = 0.0;
                var listaJobsFuncionario =
                    listaJobs.Where(e => e.Funcionario == i && e.DataEstimativa > Convert.ToDateTime(primeiraData));
                var cargaHoraria = Convert.ToInt16(i.QuantidadeHora);
                var diferencaDias = Convert.ToDateTime(segundaData) - Convert.ToDateTime(primeiraData);
                horasFuncionarioPeriodo = cargaHoraria *
                                          diferencaDias.Days;
                foreach (var j in listaJobsFuncionario)
                {
                    var horas = ConverterHorasDouble(j.HorasNecessarias);
                    var diferencaDiasJob = j.DataEstimativa - j.DataCriacao;
                    hrsPorDia = horas / Convert.ToDouble(diferencaDiasJob.Days);
                    quantidadeDias = Convert.ToDateTime(segundaData) > j.DataEstimativa
                        ? (j.DataEstimativa - Convert.ToDateTime(primeiraData)).Days
                        : diferencaDias.Days;
                    horasPeriodo += hrsPorDia * quantidadeDias;
                }
                var retorno = horasFuncionarioPeriodo - horasPeriodo;
                lista.Add(new ObjetoFuncionarioDisponibilidade
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    HorasDouble = retorno,
                    HorasDisponiveis = ConverterHorasString(retorno)
                });
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public double ConverterHorasDouble(string hora)
        {
            var horaSplit = hora.Split(':');
            var minutos = Convert.ToDouble(horaSplit[1]);
            var minutosFinal = minutos / 60;
            var horas = Convert.ToInt32(horaSplit[0]);
            var retorno = Convert.ToDouble(horas + minutosFinal);
            return retorno;
        }

        public string ConverterHorasString(double hora)
        {
            var horaString = hora.ToString().Split(',');
            var horas = horaString[0];
            var minutosString = horaString[1].Substring(0, 2);
            var minutosInt = Convert.ToInt64(minutosString);
            var minutos = horaString[1].StartsWith("0") || horaString[1].Length != 1
                ? (minutosInt * 60) / 100
                : ((minutosInt * 10) * 60) / 100;
            var horaFinal = minutos.ToString().Length != 1 ? horas + ":" + minutos.ToString().Substring(0, 2) : horas + ":0" + minutos;
            return horaFinal;
        }

        public JsonResult ValidaCpf(string id)
        {
            var listaFuncionario = _funcionarioService.Listar(e => true);
            return Json(listaFuncionario.Any(e => e.Cpf == id) ? "existente" : "valido", JsonRequestBehavior.AllowGet);
        }
    }
}
