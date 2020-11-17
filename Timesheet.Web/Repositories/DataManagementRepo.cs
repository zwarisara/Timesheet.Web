using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using Timesheet.Web.EF;
using static Timesheet.Web.Models.DataManagementModel;

namespace Timesheet.Web.Repositories
{
    public class DataManagementRepo
    {
        public bool Insert(DataTable dt)
        {
            bool result = false;
            try
            {
                string strFullName = dt.AsEnumerable().FirstOrDefault(i => i.Field<string>("PERFORMANCE EVALUATION REPORT").Equals("NAME")).Field<string>("F2").ToString();
                string strMonthOf = dt.AsEnumerable().FirstOrDefault(i => i.Field<string>("PERFORMANCE EVALUATION REPORT").Equals("MONTH OF")).Field<string>("F2").ToString();
                var dtData = dt.AsEnumerable().Where(i => !string.IsNullOrEmpty(i.Field<string>("F4")) && i.Field<string>("F2") != "Date");
                string strDateOf = "";

                TB_WEEKLY_TIMESHEET data = new TB_WEEKLY_TIMESHEET();
                using (DB_TIMESHEETEntities db = new DB_TIMESHEETEntities())
                {
                    foreach (DataRow row in dtData)
                    {
                        data.EMPLOYEE_NAME = strFullName;
                        data.MONTH_OF = strMonthOf;
                        data.DATE_OF = string.IsNullOrEmpty(row["F2"].ToString()) ? int.Parse(strDateOf) : int.Parse(row["F2"].ToString().Trim());
                        data.JOB_TYPE = row["F4"].ToString().Trim();
                        data.JOB_CODE = row["F5"].ToString().Trim();
                        data.JOB_NAME = row["F6"].ToString().Trim();
                        data.PROJECT_NAME = row["F7"].ToString().Trim();
                        data.INCIDENT_NO = row["F8"].ToString().Trim();
                        data.DESCRIPTION = row["F9"].ToString().Trim();
                        data.PROGRAM_NAME = row["F10"].ToString().Trim();
                        data.WORK_HOUR = (string.IsNullOrEmpty(row["F11"].ToString().Trim()) || row["F11"].ToString().Trim() == "-") ? 0 : decimal.Parse(row["F11"].ToString().Trim());
                        data.PROJECT_MANAGER = row["F12"].ToString().Trim();
                        data.DEPARTMENT = row["F13"].ToString().Trim();
                        data.WORK_LOCATION = row["F14"].ToString().Trim();
                        data.ACTIVE = true;
                        data.UPDATE_BY = "Admin";
                        data.UPDATE_DATE = DateTime.Now;

                        strDateOf = data.DATE_OF.Value.ToString();

                        db.TB_WEEKLY_TIMESHEET.Add(data);
                        db.SaveChanges();
                        result = true;

                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        public void RemoveOldData()
        {
            using (DB_TIMESHEETEntities db = new DB_TIMESHEETEntities())
            {
                List<TB_WEEKLY_TIMESHEET> removeOldData = db.TB_WEEKLY_TIMESHEET.ToList();
                db.TB_WEEKLY_TIMESHEET.RemoveRange(removeOldData);
                db.SaveChanges();
            }

        }

        public List<ListModel> GetList(string name)
        {
            List<ListModel> lst = new List<ListModel>();
            using (DB_TIMESHEETEntities db = new DB_TIMESHEETEntities())
            {
                List<SP_GET_LIST_TIMESHEET_Result> data = db.SP_GET_LIST_TIMESHEET(name).ToList();
                lst = data.Select(i => new ListModel()
                {
                    EMPLOYEE_NAME = i.EMPLOYEE_NAME,
                    COUNT_JOB_CODE_PROJECT = i.PROJECT_JOB_CODE.ToString(),
                    HOUR_JOB_CODE_SUPPORT = double.Parse(i.SUPPORT_WORK_HOUR.ToString()),
                    HOUR_JOB_CODE_PROJECT = double.Parse(i.PROJECT_WORK_HOUR.ToString()),
                    AVERAGE_JOB_CODE_SUPPORT = double.Parse(i.SUPPORT_PERCENT.ToString()),
                    AVERAGE_JOB_CODE_PROJECT = double.Parse(i.PROJECT_PERCENT.ToString()),
                    LAST_UPDATE_DATE = i.LAST_UPDATE_DATE.ToString()
                }).ToList();
            }

            return lst;

        }

        public DataTable GetDataForExport()
        {
            DatabaseHelper db = new DatabaseHelper();
            return db.ExecuteDataTable("SP_GET_LIST_TIMESHEET");
        }
    }
}