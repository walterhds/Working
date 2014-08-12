using System.Web.Mvc;
using Percistencia.Ado;

namespace Working.NHibernate
{
    public class TransactionPerRequest : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            NHibernateHelper.BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                NHibernateHelper.CommitTransaction();
            }

            NHibernateHelper.DisposeSession();
        }
    }
}