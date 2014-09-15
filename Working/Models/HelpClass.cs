using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;
using Microsoft.Ajax.Utilities;

namespace Working.Models
{
    public static class HelpClass
    {
        private static FuncionarioService _funcionarioService;

        public static Funcionario UsuarioLogado()
        {
            _funcionarioService = Dependencia.Resolver<FuncionarioService>();
            return _funcionarioService.ObterPorLogin((string) System.Web.HttpContext.Current.Session["usuario"]);
        }

        public static string RemoverAcentos(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return String.Empty;
            else
            {
                byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(texto);
                return System.Text.Encoding.UTF8.GetString(bytes);
            }
        }

        public static string MenuCadastros(Funcionario funcionario)
        {
            var fun = funcionario.Cargo.Nome.Replace(" ","");
            fun = RemoverAcentos(fun);
            var pv = "_MenuCadastros" + fun;
            return pv;
        }
    }
}