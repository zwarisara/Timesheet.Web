using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timesheet.Web.Models;
using Timesheet.Web.Repositories;

namespace Timesheet.Web.Controllers
{
    public class TimesheetController : Controller
    {
        private static TimesheetRepo _TimeSheetRepo = new TimesheetRepo();
        public ActionResult Index()
        {
            TimesheetModel model = new TimesheetModel();
            model.DATE_OF = DateTime.Now;
            model.JOB_CODE_LIST = _TimeSheetRepo.GetAllJobCode();
            model.SUB_JOB_CODE_LIST = _TimeSheetRepo.GetAllSubJobCode();
            return View(model);
        }

        [HttpPost]
        public JsonResult List(TimesheetModel param)
        {
            TimeSheetLoginUser user = LoginRepo.GetOwnerUser();
            List<TimesheetModel> lstModel = _TimeSheetRepo.GetList(user.id);
            return Json(lstModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetJobCodeName(string job_id)
        {
            JobCodeListModel data  = _TimeSheetRepo.GetAllJobCode(job_id).FirstOrDefault();
            return Json(new { Name = data.JOBCODE_NAME }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetSubJobCode(string job_id)
        {
            List<SubJobCodeListModel> data = _TimeSheetRepo.GetAllSubJobCode(job_id);
            return Json(new { Result = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveTimeSheet(TimesheetModel param)
        {
            param.EMPLOYEE_ID = LoginRepo.GetOwnerUser().id;
            bool result = _TimeSheetRepo.Insert(param);

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
    }
}