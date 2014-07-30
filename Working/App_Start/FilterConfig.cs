using System.Web.Mvc;
using Working.NHibernate;

namespace Working
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TransactionPerRequest());
        }
    }
}