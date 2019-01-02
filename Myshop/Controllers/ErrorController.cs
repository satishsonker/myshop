using Myshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace Myshop.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult SetView(string message)
        {
            ViewBag.message = TempData["messages"];
            return View("Details");
        }

        public ActionResult SetDowntime(string message)
        {
            ViewBag.message = TempData["messages"];
            return View("Downtime");
        }

        public ActionResult SessionExpired(string message)
        {
            TempData["title"]= message ?? "Session Expire";
            return View("SessionExpired");
        }

        public ActionResult NotFound()
        {
            return View("Error", GetErrorModel(GlobalResource.Resource.NotFound_404, GlobalResource.Resource.HttpStatus_NotFound, HttpStatusCode.NotFound));
        }

        public ActionResult UnAuthorized()
        {
            return View("Error", GetErrorModel(GlobalResource.Resource.UnAuthorized_401, GlobalResource.Resource.HttpStatus_UnAuthorized, HttpStatusCode.Unauthorized));
        }

        public ActionResult Error()
        {
            return View(GetErrorModel(GlobalResource.Resource.InternalServerError_500, GlobalResource.Resource.HttpStatus_InternalServerError, HttpStatusCode.InternalServerError));
        }

        private ErrorModel GetErrorModel(string ErrorMsg, string Title, HttpStatusCode code)
        {
            ErrorModel model = new ErrorModel();
            model.StatusCode = (int)code;
            model.Path = Request["aspxerrorpath"] == null ? string.Empty : Request["aspxerrorpath"].ToString();
            model.ErrorMessage = ErrorMsg;
            model.Title = Title;
            return model;
        }
    }
}