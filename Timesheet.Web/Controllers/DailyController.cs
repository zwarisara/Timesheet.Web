using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timesheet.Web.Models;

namespace Timesheet.Web.Controllers
{
    public class DailyController : Controller
    {
        // GET: Daily
        public ActionResult Index()
        {
            DailyModel model = new DailyModel();
            return View(model);
        }
    }
}