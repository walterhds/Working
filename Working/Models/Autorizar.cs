using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dependencias;
using Dominio.Servicos;

namespace Working.Models
{
    public class AutorizarAttribute : AuthorizeAttribute
    {
        private readonly FuncionarioService _funcionarioService;

        public AutorizarAttribute()
        {
            _funcionarioService = Dependencia.Resolver<FuncionarioService>();
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            var usuario = _funcionarioService.ObterPorLogin((string) HttpContext.Current.Session["usuario"]);

            if (Roles == "" && usuario != null)
            {
                return true;
            }

            var funcionalidades = Roles.Replace(", ", ",").Split(',');

            foreach (var i in funcionalidades)
            {
                if (usuario != null && usuario.TemAcesso(i))
                {
                    return true;
                }
            }

            return false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result=new RedirectResult("~/Login/Logon");
                return;
            }

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result=new RedirectResult("~/Login/AcessoNegado");
                return;
            }
        }
    }
}