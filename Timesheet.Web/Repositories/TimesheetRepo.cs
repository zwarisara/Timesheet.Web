using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Timesheet.Web.EF;
using Timesheet.Web.Models;

namespace Timesheet.Web.Repositories
{
    public class TimesheetRepo
    {
        public List<JobCodeListModel> GetAllJobCodeTest(string job_code)
        {
            using (DB_TIMESHEETEntities db = new DB_TIMESHEETEntities())
            {
                List<JobCodeListModel> lst = db.TB_JOBCODE.Select(i => new JobCodeListModel
                { 
                    JOBCODE_ID = i.JOBCODE_ID.ToString(),
                    JOBCODE_NO = i.JOBCODE_NO,
                    JOBCODE_NAME = i.JOBCODE_NAME
                }).ToList();

                if (!string.IsNullOrEmpty(job_code)) 
                {
                    lst = lst.Where(i => i.JOBCODE_NO.Equals(job_code)).ToList();
                }

                return lst;
            }
           
        }
        public List<SubJobCodeListModel> GetAllSubJobCode()
        {
            using (DB_TIMESHEETEntities db = new DB_TIMESHEETEntities())
            {
                List<SubJobCodeListModel> lst = db.TB_PROJECT.Select(i => new SubJobCodeListModel
                {
                    PROJECT_ID = i.PROJECT_ID.ToString(),
                    PROJECT_NAME = i.PROJECT_NAME,
                    JOBCODE_NO = i.JOBCODE_NO
                }).ToList();

                return lst;
            }

        }

        public List<JobCodeListModel> GetAllJobCode(string job_code)
        {
            List<JobCodeListModel> lst = new List<JobCodeListModel>();
            using (DB_TIMESHEETEntities db = new DB_TIMESHEETEntities())
            {
                List<SP_GET_JOB_CODE_Result> data = db.SP_GET_JOB_CODE(job_code).ToList();
                lst = data.Select(i => new JobCodeListModel() {
                    JOBCODE_ID = i.JOBCODE_ID.ToString(),
                    JOBCODE_NAME = i.JOBCODE_NAME,
                    JOBCODE_NO = i.JOBCODE_NO
                
                }).ToList();
            }

            return lst;
        }
    }
}