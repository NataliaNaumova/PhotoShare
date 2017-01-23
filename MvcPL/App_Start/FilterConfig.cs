using System.Web;
using System.Web.Mvc;
using MvcPL.Infrastructure.Helpers;

namespace MvcPL
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleAllErrorsAttribute());
        }
    }
}