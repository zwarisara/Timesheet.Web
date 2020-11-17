﻿using System;
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
            model.JOB_CODE_LIST = _TimeSheetRepo.GetAllJobCode("");
            model.SUB_JOB_CODE_LIST = _TimeSheetRepo.GetAllSubJobCode();
            return View(model);
        }

        [HttpPost]
        public JsonResult GetJobCodeName(string job_code)
        {
            JobCodeListModel data  = _TimeSheetRepo.GetAllJobCode(job_code).FirstOrDefault();
            return Json(new { Name = data.JOBCODE_NAME }, JsonRequestBehavior.AllowGet);
        }
    }
}