using System.Web;
using System.Web.Mvc;

namespace Zuehlke.Zmapp.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}