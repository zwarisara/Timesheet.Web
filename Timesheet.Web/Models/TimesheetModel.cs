using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timesheet.Web.Models
{
    public class TimesheetModel
    {
        public DateTime DATE_OF { get; set; }
        public string JOB_CODE { get; set; }
        public List<JobCodeListModel> JOB_CODE_LIST { get; set; }
        public string JOB_CODE_NAME { get; set; }
        public string SUB_JOB_CODE { get; set; }
        public string TYPE { get; set; }
        public List<SubJobCodeListModel> SUB_JOB_CODE_LIST { get; set; }
        public string PROJECT_NAME { get; set; }
        public string TICKET_NO { get; set; }
        public string WORK_HOUR { get; set; }
        public string WORK_LOCATION { get; set; }
        public string DESCRIPTION { get; set; }
        public string STATUS { get; set; }
        public int EMPLOYEE_ID { get; set; }

    }
    public class JobCodeListModel
    {
        public string JOBCODE_ID { get; set; }
        public string JOBCODE_NO { get; set; }
        public string JOBCODE_NAME { get; set; }
    }

    public class SubJobCodeListModel 
    {
        public string PROJECT_ID { get; set; }
        public string PROJECT_NAME { get; set; }
        public string JOBCODE_NO { get; set; }
    }

}