//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Timesheet.Web.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_WEEKLY_TIMESHEET
    {
        public int ID { get; set; }
        public string EMPLOYEE_NAME { get; set; }
        public string MONTH_OF { get; set; }
        public Nullable<int> DATE_OF { get; set; }
        public string JOB_TYPE { get; set; }
        public string JOB_CODE { get; set; }
        public string JOB_NAME { get; set; }
        public string PROJECT_NAME { get; set; }
        public string INCIDENT_NO { get; set; }
        public string DESCRIPTION { get; set; }
        public string PROGRAM_NAME { get; set; }
        public Nullable<decimal> WORK_HOUR { get; set; }
        public string PROJECT_MANAGER { get; set; }
        public string DEPARTMENT { get; set; }
        public string WORK_LOCATION { get; set; }
        public Nullable<bool> ACTIVE { get; set; }
        public Nullable<System.DateTime> CREATE_DATE { get; set; }
        public string CREATE_BY { get; set; }
        public Nullable<System.DateTime> UPDATE_DATE { get; set; }
        public string UPDATE_BY { get; set; }
        public string REMARK { get; set; }
    }
}
