using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Percistencia.Ado;

namespace Working.App_Start.NHibernate
{
    public class SessionPerRequest : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            NHibernateHelper.DisposeSession();
        }
    }
}