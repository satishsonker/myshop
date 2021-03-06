﻿using Myshop.Controllers;
using Myshop.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.Areas.Global.Controllers
{
    [MyshopAuthorize]
    [MyShopPermission]
    public class MainController : CommonController
    {
        // GET: Global/Main
        public ActionResult Home()
        {
            return View();
        }
    }
}