﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myshop.Areas.EmployeesManagement.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: EmployeesManagement/Employee
        public ActionResult Home()
        {
            return View();
        }
    }
}