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
            if (string.IsNullOrEmpty(Session["authorized"] as string))
            {
                return RedirectToAction("index", "Login");
            }
            else
            {
                HomeModel model = new HomeModel();
                model.txtstartday = DateTime.Now;
                model.txtendday = DateTime.Now;
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult ListChartPie(string startday, string endday)
        {
            List<ListChartPieModel> lstModel = _repo.GetListChertPie(startday, endday);
            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListChartColumn(string startday , string endday)
        {
            List<ListChartColumnModel> lstModel = _repo.GetListChertColumn(startday, endday);
            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListChartColumn2()
        {
            List<ListChartColumn2Model> lstModel = _repo.GetListChertColumn2();
            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListEmployee()
        {
            List<ListEmployeeModel> lstModel = _repo.GetListEmployee();
            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }
    }
}