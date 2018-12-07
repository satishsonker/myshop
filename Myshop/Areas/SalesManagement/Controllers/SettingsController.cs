using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.Areas.SalesManagement.Controllers
{
    public class SettingsController : Controller
    {
        // GET: SalesManagement/Settings
        [HttpGet]
        public ActionResult GetSetting()
        {
            return View();
        }
    }
}