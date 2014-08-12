using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Percistencia.Ado.Repositorio;
using Working.Models;

namespace Working.Controllers
{
    public class SituacaoJobController : Controller
    {
        private readonly SituacaoJobService _situacaoJobService;
        private readonly FuncionarioService _funcionarioService;

        public SituacaoJobController()
        {
            _situacaoJobService = Dependencia.Resolver<SituacaoJobService>();
            _funcionarioService = Dependencia.Resolver<FuncionarioService>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public void CadastrarAjax(string descricao)
        {
            var situacaoJob = new SituacaoJob();
            situacaoJob.IdAlterado =
                _funcionarioService.ObterPorLogin((string) System.Web.HttpContext.Current.Session["usuario"]);
            situacaoJob.Descricao = descricao;
            situacaoJob.DataRegistro = DateTime.Now;
            _situacaoJobService.Cadastrar(situacaoJob);
        }

        public JsonResult ListarSituacaoJobJson()
        {
            var situacaoJob = _situacaoJobService.Listar(e => true);
            var lista = new List<Objeto>();

            foreach (var i in situacaoJob)
            {
                lista.Add(new Objeto
                {
                    id = i.Id,
                    Descricao = i.Descricao
                });
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}
