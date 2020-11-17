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
        public List<ListChartModel> GetListChert(string month)
        {
            List<ListChartModel> lst = new List<ListChartModel>();
            DatabaseHelper db = new DatabaseHelper();
            db.AddParameter("@month", month);
            DataTable dt = db.ExecuteDataTable("SP_GET_LIST_CHART");
            foreach (DataRow item in dt.Rows)
            {
                ListChartModel model = new ListChartModel();
                model.name = item["name"].ToString();
                model.y = Convert.ToDecimal(item["y"]);
                lst.Add(model);
            }
            return lst;

        }
    }
}