using System.Web.Mvc;

namespace Myshop.Areas.ExpenseManagement
{
    public class ExpenseManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ExpenseManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ExpenseManagement_default",
                "ExpenseManagement/{controller}/{action}/{id}",
                new { controller="ExpHome", action = "Dashboard", id = UrlParameter.Optional }
            );
        }
    }
}