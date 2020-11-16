using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timesheet.Web.Models
{
    public class TimesheetModel
    {
        public string DATE_OF { get; set; }
        //public string STATUS { get; set; }
        public string JOB_CODE { get; set; }
        public string JOB_CODE_NAME { get; set; }
        public string SUB_JOB_CODE { get; set; }
        public string PROJECT_NAME { get; set; }
        public string TICKET_NO { get; set; }
        public string WORK_HOUR { get; set; }
        public string WORK_LOCATION { get; set; }
        public string DESCRIPTION { get; set; }
        public string STATUS { get; set; }
        public string[] ALL_STATUS = new[] { "ทำงานปกติ", "ลา" };
}

}