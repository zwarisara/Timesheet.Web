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
    
    public partial class TB_TICKET
    {
        public int TICKET_NO { get; set; }
        public Nullable<System.DateTime> START_DATE { get; set; }
        public Nullable<System.DateTime> END_DATE { get; set; }
        public string STATUS { get; set; }
        public string REMARK { get; set; }
        public Nullable<int> JOBCODE_NO { get; set; }
    }
}
