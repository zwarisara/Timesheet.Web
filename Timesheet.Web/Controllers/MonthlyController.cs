using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timesheet.Web.Models;

namespace Timesheet.Web.Controllers
{
    public class MonthlyController : Controller
    {
        // GET: Month
        public ActionResult Index()
        {
            MonthlyModel model = new MonthlyModel();
            return View(model);
        }
    }
}