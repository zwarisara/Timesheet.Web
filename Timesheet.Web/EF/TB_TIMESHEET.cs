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
    
    public partial class TB_TIMESHEET
    {
        public int TIMESHEET_ID { get; set; }
        public Nullable<System.DateTime> TIMESHEET_DATE { get; set; }
        public Nullable<int> EMPLOYEE_ID { get; set; }
        public Nullable<int> JOBCODE_ID { get; set; }
        public string TICKET_ID { get; set; }
        public string TIMESHEET_REMARK { get; set; }
        public Nullable<decimal> WORK_HOUR { get; set; }
        public string WORK_LOCATION { get; set; }
        public string PROGRAM_NAME { get; set; }
        public Nullable<int> CREATED_BY { get; set; }
        public Nullable<System.DateTime> CREATED_DATE { get; set; }
        public Nullable<int> UPDATED_BY { get; set; }
        public Nullable<System.DateTime> UPDATED_DATE { get; set; }
    }
}
