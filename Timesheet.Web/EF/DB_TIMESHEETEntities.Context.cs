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
    
        public virtual DbSet<TB_WEEKLY_TIMESHEET> TB_WEEKLY_TIMESHEET { get; set; }
        public virtual DbSet<TB_EMPLOYEE> TB_EMPLOYEE { get; set; }
        public virtual DbSet<TB_EMPLOYEE_JOBCODE> TB_EMPLOYEE_JOBCODE { get; set; }
        public virtual DbSet<TB_JOBCODE> TB_JOBCODE { get; set; }
        public virtual DbSet<TB_POT> TB_POT { get; set; }
        public virtual DbSet<TB_PROJECT> TB_PROJECT { get; set; }
        public virtual DbSet<TB_TICKET> TB_TICKET { get; set; }
        public virtual DbSet<TB_TIMESHEET> TB_TIMESHEET { get; set; }
    
        public virtual ObjectResult<SP_GET_JOB_CODE_Result> SP_GET_JOB_CODE(string jOB_CODE)
        {
            var jOB_CODEParameter = jOB_CODE != null ?
                new ObjectParameter("JOB_CODE", jOB_CODE) :
                new ObjectParameter("JOB_CODE", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_JOB_CODE_Result>("SP_GET_JOB_CODE", jOB_CODEParameter);
        }
    
        public virtual ObjectResult<SP_GET_LIST_TIMESHEET_Result> SP_GET_LIST_TIMESHEET(string nAME)
        {
            var nAMEParameter = nAME != null ?
                new ObjectParameter("NAME", nAME) :
                new ObjectParameter("NAME", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GET_LIST_TIMESHEET_Result>("SP_GET_LIST_TIMESHEET", nAMEParameter);
        }
    }
}
