using Myshop.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Myshop.Filters
{
    public class MyShopPermission: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string url = filterContext.RequestContext.HttpContext.Request.Path.ToLower();
            string urlRefer = string.Empty;
            if (filterContext.RequestContext.HttpContext.Request.UrlReferrer != null)//&& filterContext.HttpContext.Request.IsAjaxRequest())
            {
                urlRefer = filterContext.RequestContext.HttpContext.Request.UrlReferrer.LocalPath.ToString().ToLower();
            }

            var permission = WebSession.Permission.Where(x => (x.Url.ToLower().Equals(url) || x.Url.ToLower().Equals(urlRefer)) && x.IsBlockAccess == false).FirstOrDefault();
            //if (permission==null)
            //{
            //    if(filterContext.HttpContext.Request.IsAjaxRequest())
            //    {
            //        filterContext.RequestContext.HttpContext.Response.Write("You don't have access permission");
            //    }
            //    filterContext.Controller.TempData["messages"] = "You are not authorize for this request";
            //    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Error", action = "SetView", area = "" }));
            //    return;
            //}
            //else
            //{
            WebSession.UserCanRead = true;// permission.Read;
            WebSession.UserCanWrite = true;// permission.Write;
            WebSession.UserCanDelete = true;// permission.Delete;
            WebSession.UserCanUpdate = true;// permission.Update;
            //}
        }
    }
}