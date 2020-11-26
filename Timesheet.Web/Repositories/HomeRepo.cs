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
    }
}