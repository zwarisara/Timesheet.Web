using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timesheet.Web.Models;

namespace Timesheet.Web.Controllers
{
    public class JobCodeController : Controller
    {
        // GET: Job Code
        public ActionResult Index()
        {
            JobCodeModel model = new JobCodeModel();
            return View(model);
        }
    }
}