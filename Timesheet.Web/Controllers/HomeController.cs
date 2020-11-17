using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timesheet.Web.Models;
using Timesheet.Web.Repositories;
using static Timesheet.Web.Models.HomeModel;

namespace Timesheet.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeRepo _repo = new HomeRepo();
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult ListChart(string month)
        {
            List<ListChartModel> lstModel = _repo.GetListChert(month);
            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

    }
}