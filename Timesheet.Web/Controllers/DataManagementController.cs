using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Timesheet.Web.Models;
using Timesheet.Web.Repositories;
using static Timesheet.Web.Models.DataManagementModel;

namespace Timesheet.Web.Controllers
{
    public class DataManagementController : Controller
    {
        // GET: LoadData
        public DataManagementRepo _repo = new DataManagementRepo();
        public ActionResult Index()
        {
            SearchModel model = new SearchModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult List(string Name)
        {
            List<ListModel> lstModel = _repo.GetList(Name);
            return Json(lstModel, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult InsertData()
        {
            //string[] filePaths = Directory.GetFiles(@"\\pttgrp-fs-s01\pttict2\AOU_P SharePoint\AOU_P SharePoint-DATA\Project Folder\Non SAP\001_รายชื่อพนักงาน Outsource AOUP\Timesheet_Weekly_Activity\Year 2020\10_Oct");
            string[] filePaths = Directory.GetFiles(@"D:\FileTimeSheet\11_Nov");
            List<string> lstString = new List<string>();
            List<string> lstErrorSave = new List<string>();
            List<string> lstErrorCatch = new List<string>();

            //Delete Old Data for New Insert
            _repo.RemoveOldData();

            foreach (string strPath in filePaths)
            {
                string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=Excel 12.0;";
                try
                {
                    using (OleDbConnection connection = new OleDbConnection(connStr))
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        OleDbDataReader dr = null;
                        try
                        {
                            command = new OleDbCommand("select * from [" + GetSheetName() + "$]", connection);
                            dr = command.ExecuteReader();
                        }
                        catch (Exception ex)
                        {
                            command = new OleDbCommand("select * from [" + GetSheetFullName() + "$]", connection);
                            dr = command.ExecuteReader();
                        }
                        DataTable dt = new DataTable();
                        dt.Load(dr);

                        //Insert Data
                        bool result = _repo.Insert(dt);
                        if (result)
                            lstString.Add(strPath);
                        else
                            lstErrorSave.Add(strPath);

                    }

                }
                catch (Exception ex)
                {
                    lstErrorCatch.Add(strPath + " Error --------->  " + ex.Message);
                    continue;

                }
            }

            return Json(new { result = true, lstData = lstString, countAllFileInDirectory = filePaths.Length, lstErrorSave = lstErrorSave, lstErrorCatch = lstErrorCatch }, JsonRequestBehavior.AllowGet);

        }

        public string GetSheetName()
        {
            string strPrefixMonth = "";
            int monthNow = DateTime.Now.Month;

            switch (monthNow)
            {
                case 1:
                    strPrefixMonth = "Jan";
                    break;
                case 2:
                    strPrefixMonth = "Feb";
                    break;
                case 3:
                    strPrefixMonth = "Mar";
                    break;
                case 4:
                    strPrefixMonth = "Apr";
                    break;
                case 5:
                    strPrefixMonth = "May";
                    break;
                case 6:
                    strPrefixMonth = "Jun";
                    break;
                case 7:
                    strPrefixMonth = "Jul";
                    break;
                case 8:
                    strPrefixMonth = "Aug";
                    break;
                case 9:
                    strPrefixMonth = "Sep";
                    break;
                case 10:
                    strPrefixMonth = "Oct";
                    break;
                case 11:
                    strPrefixMonth = "Nov";
                    break;
                case 12:
                    strPrefixMonth = "Dec";
                    break;

            }

            return strPrefixMonth;

        }

        public string GetSheetFullName()
        {
            string strPrefixMonth = "";
            int monthNow = DateTime.Now.Month;

            switch (monthNow)
            {
                case 1:
                    strPrefixMonth = "January";
                    break;
                case 2:
                    strPrefixMonth = "February";
                    break;
                case 3:
                    strPrefixMonth = "March";
                    break;
                case 4:
                    strPrefixMonth = "April";
                    break;
                case 5:
                    strPrefixMonth = "May";
                    break;
                case 6:
                    strPrefixMonth = "June";
                    break;
                case 7:
                    strPrefixMonth = "July";
                    break;
                case 8:
                    strPrefixMonth = "August";
                    break;
                case 9:
                    strPrefixMonth = "September";
                    break;
                case 10:
                    strPrefixMonth = "October";
                    break;
                case 11:
                    strPrefixMonth = "November";
                    break;
                case 12:
                    strPrefixMonth = "December";
                    break;

            }

            return strPrefixMonth;

        }

        public void ExportExcel(string param)
        {
            DataTable dt = _repo.GetDataForExport();
            string attachment = "attachment; filename=Summary_Monthly.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";

            GridView gvExcel = new GridView();
            gvExcel.DataSource = dt;
            gvExcel.DataBind();
            gvExcel.RenderControl(new HtmlTextWriter(Response.Output));

            Response.Flush();
            Response.End();
        }

    }
}