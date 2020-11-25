using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
        public List<SubJobCodeListModel> GetAllSubJobCode(string job_code_id = null)
        {
            using (DB_TIMESHEETEntities db = new DB_TIMESHEETEntities())
            {
                List<SubJobCodeListModel> lst = db.TB_JOBCODE.Where(i => i.JOBCODE_ID_PARENT.Value.ToString().Equals(job_code_id)).Select(i => new SubJobCodeListModel
                {
                    SUB_JOBCODE_ID = i.JOBCODE_ID.ToString(),
                    SUB_JOBCODE_NO = i.JOBCODE_NO,
                    SUB_JOBCODE_NAME = i.JOBCODE_NAME
                }).ToList();

                return lst;
            }

        }

        public List<JobCodeListModel> GetAllJobCode(string job_id = null)
        {
            List<JobCodeListModel> lst = new List<JobCodeListModel>();
            using (DB_TIMESHEETEntities db = new DB_TIMESHEETEntities())
            {
                List<SP_GET_JOB_CODE_Result> data = db.SP_GET_JOB_CODE(job_id).ToList();
                lst = data.Select(i => new JobCodeListModel()
                {
                    JOBCODE_ID = i.JOBCODE_ID.ToString(),
                    JOBCODE_NAME = i.JOBCODE_NAME,
                    JOBCODE_NO = i.JOBCODE_NO

                }).ToList();
            }

            return lst;
        }

        public bool Insert(TimesheetModel parameter)
        {
            bool result = false;
            try
            {
                TB_TIMESHEET data = new TB_TIMESHEET();
                using (DB_TIMESHEETEntities db = new DB_TIMESHEETEntities())
                {
                    var jobCodeID = db.TB_JOBCODE.FirstOrDefault(i => i.JOBCODE_NO.Equals(parameter.JOB_CODE)).JOBCODE_ID; 

                    data.TIMESHEET_DATE = parameter.DATE_OF;
                    data.EMPLOYEE_ID = parameter.EMPLOYEE_ID;
                    data.JOBCODE_ID = jobCodeID;
                    data.TICKET_ID = parameter.TICKET_ID;
                    data.TIMESHEET_REMARK = parameter.DESCRIPTION;
                    data.WORK_HOUR = decimal.Parse(parameter.WORK_HOUR);
                    data.WORK_LOCATION = parameter.WORK_LOCATION;

                    db.TB_TIMESHEET.Add(data);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public List<TimesheetModel> GetList(int empID)
        {
            List<TimesheetModel> lst = new List<TimesheetModel>();
            try 
            { 
                using (DB_TIMESHEETEntities db = new DB_TIMESHEETEntities())
                {
                    lst = db.TB_TIMESHEET.Where(i => i.EMPLOYEE_ID.Value.Equals(empID))
                        .Select(i => new TimesheetModel() {
                            EMPLOYEE_ID = i.EMPLOYEE_ID.Value,
                            Str_DATE_OF = i.TIMESHEET_DATE.Value.ToString(),
                            DATE_OF = i.TIMESHEET_DATE.Value,
                            JOB_CODE = db.TB_JOBCODE.FirstOrDefault(j => j.JOBCODE_ID.Equals(i.JOBCODE_ID.Value)).JOBCODE_NO,
                            JOB_CODE_NAME = db.TB_JOBCODE.FirstOrDefault(j => j.JOBCODE_ID.Equals(i.JOBCODE_ID.Value)).JOBCODE_NAME,
                            SUB_JOB_CODE = "",
                            TICKET_NO = i.TICKET_ID,
                            WORK_HOUR = i.WORK_HOUR.ToString(),
                            WORK_LOCATION = i.WORK_LOCATION
                        }).ToList();
                }
            }
            catch (Exception ex) 
            {
            
            }

            return lst;

        }
    }
}