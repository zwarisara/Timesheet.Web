using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using static Timesheet.Web.Models.HomeModel;

namespace Timesheet.Web.Repositories
{
    public class HomeRepo
    {
        public List<ListChartPieModel> GetListChertPie(string startday, string endday)
        {
            List<ListChartPieModel> lst = new List<ListChartPieModel>();
            DatabaseHelper db = new DatabaseHelper();
            db.AddParameter("@startday", startday);
            db.AddParameter("@endday", endday);
            DataTable dt = db.ExecuteDataTable("SP_GET_LIST_CHART_PIE");
            foreach (DataRow item in dt.Rows)
            {
                ListChartPieModel model = new ListChartPieModel();
                model.name = item["name"].ToString();
                model.y = Convert.ToDecimal(item["y"]);
                lst.Add(model);
            }
            return lst;
        }

        public List<ListChartColumnModel> GetListChertColumn(string startday, string endday)
        {
            startday = Convert.ToDateTime(startday).ToString("yyyy-MM-dd");
            endday = Convert.ToDateTime(endday).ToString("yyyy-MM-dd");

            List<ListChartColumnModel> lst = new List<ListChartColumnModel>();
            DatabaseHelper db = new DatabaseHelper();
            db.AddParameter("@startday", startday);
            db.AddParameter("@endday", endday);
            DataTable dt = db.ExecuteDataTable("SP_GET_LIST_CHART_COLUMN");
            foreach (DataRow item in dt.Rows)
            {
                ListChartColumnModel model = new ListChartColumnModel();
                model.DATE_OF =  item["DATE_OF"].ToString();
                model.JOB_GROUP = item["JOB_GROUP"].ToString();
                model.MAN_DAY = Convert.ToDecimal(item["MAN_DAY"]);
                lst.Add(model);
            }
            return lst;
        }


        public List<ListChartColumn2Model> GetListChertColumn2()
        {
            List<ListChartColumn2Model> lst = new List<ListChartColumn2Model>();
            DatabaseHelper db = new DatabaseHelper();

            DataTable dt = db.ExecuteDataTable("SP_GET_LIST_CHART_COLUMN2");
            foreach (DataRow item in dt.Rows)
            {
                ListChartColumn2Model model = new ListChartColumn2Model();
                model.FULLNAME_EN = item["FULLNAME_EN"].ToString();
                model.JOB_GROUP = item["JOB_GROUP"].ToString();
                model.MAN_DAY = Convert.ToDecimal(item["MAN_DAY"]);
                lst.Add(model);
            }
            return lst;
        }

        public List<ListEmployeeModel> GetListEmployee()
        {
            List<ListEmployeeModel> lst = new List<ListEmployeeModel>();
            DatabaseHelper db = new DatabaseHelper();

            DataTable dt = db.ExecuteDataTable("SP_GET_LIST_EMPLOYEE");
            foreach (DataRow item in dt.Rows)
            {
                ListEmployeeModel model = new ListEmployeeModel();
                model.FULLNAME_EN = item["FULLNAME_EN"].ToString();
                lst.Add(model);
            }
            return lst;
        }
    }
}