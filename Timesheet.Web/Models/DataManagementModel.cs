using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timesheet.Web.Models
{
    public class DataManagementModel
    {
        public class SearchModel
        {
            public string _Date { get; set; }
            public string _Name { get; set; }
        }
        public class ListModel
        {
            public string EMPLOYEE_NAME { get; set; }
            public string MONTH_OF { get; set; }
            public string DATE_OF { get; set; }
            public string JOB_TYPE { get; set; }
            public string JOB_CODE { get; set; }
            public string JOB_NAME { get; set; }
            public string PROJECT_NAME { get; set; }
            public string INCIDENT_NO { get; set; }
            public string DESCRIPTION { get; set; }
            public string PROGRAM_NAME { get; set; }
            public string WORK_HOUR { get; set; }
            public string PROJECT_MANAGER { get; set; }
            public string DEPARTMENT { get; set; }
            public string WORK_LOCATION { get; set; }
            public string COUNT_JOB_CODE_PROJECT { get; set; }
            public double HOUR_JOB_CODE_SUPPORT { get; set; }
            public double HOUR_JOB_CODE_PROJECT { get; set; }
            public double AVERAGE_JOB_CODE_SUPPORT { get; set; }
            public double AVERAGE_JOB_CODE_PROJECT { get; set; }
            public string LAST_UPDATE_DATE { get; set; }

        }
    }
}