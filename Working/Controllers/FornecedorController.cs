using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Working.Models;

namespace Working.Controllers
{
    public class FornecedorController : Controller
    {
        private readonly FornecedorService _fornecedorService;

        public FornecedorController()
        {
            _fornecedorService = Dependencia.Resolver<FornecedorService>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public void CadastrarAjax(string nome, string telefone, string contato, string celular, string email)
        {
            var fornecedor = new Fornecedor();
            fornecedor.Nome = nome;
            fornecedor.Telefone = telefone;
            fornecedor.Contato = contato;
            fornecedor.Celular = celular;
            fornecedor.Email = email;
            fornecedor.DataRegistro = DateTime.Now;
            _fornecedorService.Cadastrar(fornecedor);
        }

        public JsonResult ListarFornecedoresJson()
        {
            var fornecedores = _fornecedorService.Listar(e => true);
            var lista = new List<Objeto>();
            foreach (var i in fornecedores)
            {
                lista.Add(new Objeto
                {
                    id = i.Id,
                    nome = i.Nome
                });
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult PopularModal(int id)
        {
            var fornecedor = _fornecedorService.ObterPorId(id);
            Objeto lista = new Objeto();
            lista.id = fornecedor.Id;
            lista.nome = fornecedor.Nome;
            lista.Telefone = fornecedor.Telefone;
            lista.Contato = fornecedor.Contato;
            lista.Celular = fornecedor.Celular;
            lista.Email = fornecedor.Email;
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
    }
}
