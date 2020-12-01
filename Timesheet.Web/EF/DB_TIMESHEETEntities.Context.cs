﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DB_TIMESHEETEntities : DbContext
    {
        public DB_TIMESHEETEntities()
            : base("name=DB_TIMESHEETEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TB_EMPLOYEE> TB_EMPLOYEE { get; set; }
        public virtual DbSet<TB_EMPLOYEE_JOBCODE> TB_EMPLOYEE_JOBCODE { get; set; }
        public virtual DbSet<TB_JOBCODE> TB_JOBCODE { get; set; }
        public virtual DbSet<TB_POT> TB_POT { get; set; }
        public virtual DbSet<TB_TICKET> TB_TICKET { get; set; }
        public virtual DbSet<TB_TIMESHEET> TB_TIMESHEET { get; set; }
        public virtual DbSet<TB_WEEKLY_TIMESHEET> TB_WEEKLY_TIMESHEET { get; set; }
    
        public virtual ObjectResult<SP_GET_LIST_CHART_Result> SP_GET_LIST_CHART(string startday, string endday)
        {
            var startdayParameter = startday != null ?
                new ObjectParameter("startday", startday) :
                new ObjectParameter("startday", typeof(string));
    
            var enddayParameter = endday != null ?
                new ObjectParameter("endday", endday) :
                new ObjectParameter("endday", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_LIST_CHART_Result>("SP_GET_LIST_CHART", startdayParameter, enddayParameter);
        }
    
        public virtual ObjectResult<string> SP_GET_LIST_CHART_COLUMN(string startday, string endday)
        {
            var startdayParameter = startday != null ?
                new ObjectParameter("startday", startday) :
                new ObjectParameter("startday", typeof(string));
    
            var enddayParameter = endday != null ?
                new ObjectParameter("endday", endday) :
                new ObjectParameter("endday", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("SP_GET_LIST_CHART_COLUMN", startdayParameter, enddayParameter);
        }
    
        public virtual ObjectResult<SP_GET_LIST_TIMESHEET_Result> SP_GET_LIST_TIMESHEET(string nAME)
        {
            var nAMEParameter = nAME != null ?
                new ObjectParameter("NAME", nAME) :
                new ObjectParameter("NAME", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_LIST_TIMESHEET_Result>("SP_GET_LIST_TIMESHEET", nAMEParameter);
        }
    
        public virtual ObjectResult<SP_GET_JOB_CODE_Result> SP_GET_JOB_CODE(string jOB_ID)
        {
            var jOB_IDParameter = jOB_ID != null ?
                new ObjectParameter("JOB_ID", jOB_ID) :
                new ObjectParameter("JOB_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_JOB_CODE_Result>("SP_GET_JOB_CODE", jOB_IDParameter);
        }
    
        public virtual ObjectResult<SP_GET_LIST_TIMESHEET_MONTH_Result> SP_GET_LIST_TIMESHEET_MONTH(string employee_ID)
        {
            var employee_IDParameter = employee_ID != null ?
                new ObjectParameter("Employee_ID", employee_ID) :
                new ObjectParameter("Employee_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_LIST_TIMESHEET_MONTH_Result>("SP_GET_LIST_TIMESHEET_MONTH", employee_IDParameter);
        }
    }
}
