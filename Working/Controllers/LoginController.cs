using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Dependencias;
using Dominio.Entidades;
using Dominio.Servicos;

namespace Working.Controllers
{
    public class LoginController : Controller
    {
        private readonly FuncionarioService _funcionarioService;
        private readonly ClienteService _clienteService;

        public LoginController()
        {
            _clienteService = Dependencia.Resolver<ClienteService>();
            _funcionarioService = Dependencia.Resolver<FuncionarioService>();
        }

        public ActionResult Logon()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Logon(FormCollection form)
        {
            Funcionario funcionario = _funcionarioService.ObterPorLogin(form["login"]);
            Cliente cliente = _clienteService.ObterPorLogin(form["senha"]);

            if (funcionario != null && funcionario.Senha == form["senha"])
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(funcionario.Nome, false);
                return RedirectToAction("Index", "Home");
            }
            else if ((cliente != null && cliente.Senha == form["senha"]))
            {
                System.Web.Security.FormsAuthentication.SetAuthCookie(cliente.Nome, false);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Logon", "Login");
        }
    }
}
