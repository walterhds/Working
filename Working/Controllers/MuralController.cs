using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using NHibernate.Criterion;
using Working.Models;

namespace Working.Controllers
{
    public class MuralController : Controller
    {
        private readonly MuralService _muralService;
        private readonly FuncionarioService _funcionarioService;

        public MuralController()
        {
            _muralService = Dependencia.Resolver<MuralService>();
            _funcionarioService = Dependencia.Resolver<FuncionarioService>();
        }

        public ActionResult Index()
        {
            var comentarios = _muralService.Listar(e => true);
            return View(comentarios);
        }

        public void Cadastrar(string comentario)
        {
            var mural = new Mural();
            mural.DataRegistro = DateTime.Now;
            mural.Funcionario = _funcionarioService.ObterPorLogin((string) System.Web.HttpContext.Current.Session["usuario"]);
            mural.Comentario = comentario;
            _muralService.Cadastrar(mural);
        }

        public JsonResult ListarMural()
        {
            var mural = _muralService.Listar(e => true).OrderByDescending(e=>e.DataRegistro);
            var lista = new List<Objeto>();

            foreach (var i in mural)
            {
                lista.Add(new Objeto
                {
                    id = i.Id,
                    Descricao = i.Comentario,
                    Data = i.DataRegistro.ToShortDateString(),
                    Hora = i.DataRegistro.Hour + ":" + i.DataRegistro.Minute,
                    idFuncionario = i.Funcionario.Id,
                    IdLogado = _funcionarioService.ObterPorLogin((string) System.Web.HttpContext.Current.Session["usuario"]).Id,
                    nome = i.Funcionario.Nome
                });
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        } 
    }
}
