using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using Castle.Core;
using Dependencias;
using Dominio.Servicos;
using Rotativa;
using Working.Models;
using Working.ViewsModels;

namespace Working.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly FuncionarioService _funcionarioService;
        private readonly JobService _jobService;
        private readonly ParcelasPagarService _parcelasPagarService;
        private readonly ParcelasReceberService _parcelasReceberService;
        private readonly ContasPagarService _contasPagarService;

        public RelatorioController()
        {
            _funcionarioService = Dependencia.Resolver<FuncionarioService>();
            _jobService = Dependencia.Resolver<JobService>();
            _parcelasPagarService = Dependencia.Resolver<ParcelasPagarService>();
            _parcelasReceberService = Dependencia.Resolver<ParcelasReceberService>();
            _contasPagarService = Dependencia.Resolver<ContasPagarService>();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var tipoRelatorio = form.Get("Submit");
            var primeiraData = Request["PrimeiraData"];
            var segundaData = Request["SegundaData"];
            var pdf = new ViewAsPdf();

            switch (tipoRelatorio)
            {
                case "Disponibilidade":
                    {
                        var lista = ListarDisponibilidades(primeiraData, segundaData);
                        pdf = new ViewAsPdf
                        {
                            ViewName = "RelatorioDisponibilidades",
                            Model = lista
                        };
                    }
                    break;
                case "JobsAbertos":
                {
                        var lista = new ObjetoJobRelatorios
                        {
                            Jobs = _jobService.Listar(e =>
                                e.DataEstimativa >= Convert.ToDateTime(primeiraData) &&
                                e.DataEstimativa <= Convert.ToDateTime(segundaData) && e.Fase != "ENTREGUE"),
                            PrimeiraData = primeiraData,
                            SegundaData = segundaData
                        };
                        pdf = new ViewAsPdf
                        {
                            ViewName = "RelatorioJobsAbertos",
                            Model = lista
                        };
                    }
                    break;
                case "JobsEntregues":
                {
                        var lista = new ObjetoJobRelatorios
                        {
                            Jobs = _jobService.Listar(e =>
                                e.DataEntrega >= Convert.ToDateTime(primeiraData) &&
                                e.DataEntrega <= Convert.ToDateTime(segundaData) && e.Fase == "ENTREGUE"),
                            PrimeiraData = primeiraData,
                            SegundaData = segundaData
                        };
                        pdf = new ViewAsPdf
                        {
                            ViewName = "RelatorioJobsEntregues",
                            Model = lista
                        };
                    }
                    break;
                case "ContasPagar":
                    {
                        var lista =
                            _parcelasPagarService.Listar(
                                e =>
                                    e.DataVencimento >= Convert.ToDateTime(primeiraData) &&
                                    e.DataVencimento <= Convert.ToDateTime(segundaData) && e.Situacao != "Paga");
                        pdf = new ViewAsPdf
                        {
                            ViewName = "RelatorioContasPagar",
                            Model = lista
                        };
                    }
                    break;
                case "ContasReceber":
                    {
                        var lista = _parcelasReceberService.Listar(
                            e =>
                                e.DataVencimento >= Convert.ToDateTime(primeiraData) &&
                                e.DataVencimento <= Convert.ToDateTime(segundaData) && e.Situacao != "Recebida" &&
                                e.Situacao != "Cancelada");
                        pdf = new ViewAsPdf
                        {
                            ViewName = "RelatorioContasReceber",
                            Model = lista
                        };
                    }
                    break;
                case "MeusJobs":
                    {
                        var lista =
                            _jobService.Listar(
                                e =>
                                    e.Funcionario ==
                                    _funcionarioService.ObterPorLogin(
                                        (string)System.Web.HttpContext.Current.Session["usuario"]) && e.Fase != "ENTREGUE" &&
                                    e.DataEstimativa >= Convert.ToDateTime(primeiraData) &&
                                    e.DataEstimativa <= Convert.ToDateTime(segundaData));
                        pdf = new ViewAsPdf
                        {
                            ViewName = "RelatorioMeusJobs",
                            Model = lista
                        };
                    }
                    break;
            }

            return pdf;
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
            var minutosString = "";
            try
            {
                minutosString = horaString[1].Substring(0, 2);
            }
            catch (Exception e)
            {
                minutosString = "0";
            }
            var minutosInt = Convert.ToInt64(minutosString);
            var minutos = Convert.ToString(minutosInt).StartsWith("0") || Convert.ToString(minutosInt).Length != 1
                ? (minutosInt * 60) / 100
                : ((minutosInt * 10) * 60) / 100;
            var horaFinal = minutos.ToString().Length != 1 ? horas + ":" + minutos.ToString().Substring(0, 2) : horas + ":0" + minutos;
            return horaFinal;
        }

        public List<ObjetoFuncionarioDisponibilidade> ListarDisponibilidades(string primeiraData, string segundaData)
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
                    HorasDisponiveis = ConverterHorasString(retorno),
                    PrimeiraData = primeiraData,
                    SegundaData = segundaData
                });
            }

            return lista;
        }
    }
}
