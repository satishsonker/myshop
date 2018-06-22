using System.Web;
using System.Web.Mvc;
using Myshop.Filters;

namespace Myshop
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new MyshopAuthorize());
            filters.Add(new MyshopErrorAttribute());
        }
    }
}
