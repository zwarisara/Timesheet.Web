using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timesheet.Web.Models
{
    public class HomeModel
    {
        public class ListChartPieModel
        {
            public string name { get; set; }

            public decimal y { get; set; }

            //public string JOB_CODE { get; set; }
            //public string JOBCODE_NAME { get; set; }          
            //public string BUDGET_AMOUNT { get; set; }         
        }

        public class ListChartColumnModel
        {
            public int DATE_OF { get; set; }
            public string JOB_GROUP { get; set; }
            public decimal MAN_DAY { get; set; }
        }

    }
}