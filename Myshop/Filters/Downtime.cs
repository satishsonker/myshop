using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Myshop.App_Start;
using System.Web;
using System.Web.Routing;
using DataLayer;

namespace Myshop.Filters
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class MyshopDowntime : AuthorizeAttribute, IAuthorizationFilter
    {
       
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            MyshopDb myShop = new MyshopDb();
            var downtime = myShop.Gbl_AppDowntime.Where(x => x.IsDeleted == false && x.DownTimeEnd >= DateTime.Now).FirstOrDefault();
            if (downtime != null)
            {
                WebSession.DowntimeEnd = downtime.DownTimeEnd;
                WebSession.DowntimeStart = downtime.DownTimeStart;
                WebSession.DowntimeMessage = downtime.Message;
            }
            if (!string.IsNullOrEmpty(WebSession.DowntimeMessage))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "SetDowntime", area = "" }));
                filterContext.Controller.TempData["message"] = WebSession.DowntimeMessage;
                filterContext.Controller.TempData["DowntimeStart"] = WebSession.DowntimeStart.ToString("dd-MMM-yyyy HH:mm:ss");
                filterContext.Controller.TempData["DowntimeEnd"] = WebSession.DowntimeEnd.ToString("dd-MMM-yyyy HH:mm:ss");
                return;
            }
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}