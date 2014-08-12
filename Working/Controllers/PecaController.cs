using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Microsoft.Ajax.Utilities;
using NHibernate.Hql.Ast.ANTLR;
using Working.Models;
using Working.ViewsModels;

namespace Working.Controllers
{
    public class PecaController : Controller
    {
        private readonly PecaService _pecaService;
        private readonly FornecedorService _fornecedorService;

        public PecaController()
        {
            _pecaService = Dependencia.Resolver<PecaService>();
            _fornecedorService = Dependencia.Resolver<FornecedorService>();
        }

        public ActionResult Index()
        {
            return View();
        }

        public void CadastrarAjax(string descricao, string fornecedores)
        {
            var peca = new Peca();
            var fornecedoresId = fornecedores.Split(',');
            IList<Fornecedor> listaFornecedores = new List<Fornecedor>();

            foreach (var i in fornecedoresId)
            {
                var fornecedor = _fornecedorService.ObterPorId(Convert.ToInt16(i));
                listaFornecedores.Add(fornecedor);
                peca.Fornecedor = listaFornecedores;
            }

            peca.Descricao = descricao;
            peca.DataRegistro = DateTime.Now;
            _pecaService.Cadastrar(peca);
        }

        public JsonResult ListarPecasJson(string id)
        {
            var fornecedoresId = id.Split(',');
            var lista = new List<Objeto>();
            var cont = 0;
            foreach (var i in fornecedoresId)
            {
                var fornecedor = _fornecedorService.ObterPorId(Convert.ToInt16(i));
                var pecasFornecedor = _pecaService.Listar(e => e.Fornecedor.Contains(fornecedor));

                if (cont == 0)
                {
                    foreach (var j in pecasFornecedor)
                    {
                        lista.Add(new Objeto
                        {
                            id = j.Id,
                            Descricao = j.Descricao
                        });
                    }
                    cont++;
                }
                else
                {
                    foreach (var k in pecasFornecedor)
                    {
                        if (lista.Count(objeto => objeto.id == k.Id) == 0)
                        {
                            lista.Add(new Objeto
                            {
                                id = k.Id,
                                Descricao = k.Descricao
                            });
                        }
                    }
                }
            }

            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarPecasBuscarJson()
        {
            var pecas = _pecaService.Listar(e => true);
            var lista = new List<Objeto>();

            foreach (var i in pecas)
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
