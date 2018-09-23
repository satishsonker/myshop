using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Myshop.App_Start;
using System.Web;
using System.Web.Routing;

namespace Myshop.Filters
{
    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class MyshopAuthorize : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] allowedroles;
        public MyshopAuthorize(params string[] roles)
        {
            this.allowedroles = roles;
        }
        public MyshopAuthorize()
        {

        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!string.IsNullOrEmpty(WebSession.DowntimeMessage))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "SetDowntime", area = "" }));
                filterContext.Controller.TempData["message"] = WebSession.DowntimeMessage;
                filterContext.Controller.TempData["DowntimeStart"] = WebSession.DowntimeStart.ToString("dd-MMM-yyyy");
                filterContext.Controller.TempData["DowntimeEnd"] = WebSession.DowntimeEnd.ToString("dd-MMM-yyyy"); ;
                return;
            }
            else if(string.IsNullOrEmpty(WebSession.Username)) //Session Expired
            {
filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "SessionExpired", area=""}));
               
                return;
            }
            else if (WebSession.UserId < 1)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "UnAuthorized", area=""}));
               
                return;
            }
            else if (WebSession.ShopList.Count < 1)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "login", action = "NoShopMapped", area = "" }));

                return;
            }
            if (this.allowedroles!=null && this.allowedroles.Length > 0)
            {
                if (!this.allowedroles.Contains(WebSession.UserType))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "SetView",area="" }));
                     filterContext.Controller.TempData["message"] = "You are not authorized for this resource.";
                    return;
                }
            }
           
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}