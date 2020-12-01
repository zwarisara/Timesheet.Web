using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Timesheet.Web.Models;
using Timesheet.Web.Repositories;

namespace Timesheet.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(String returnUrl, string strResult)
        {
            ViewBag.Result = strResult;
            return View(new Timesheet.Web.Models.LoginView() { returnUrl = returnUrl });
        }

        [HttpPost]
        public ActionResult Login(LoginView formData)
        {
            if (ModelState.IsValid)
            {
                AuthorizeService ad = new AuthorizeService();
                bool result = ad.CheckAuthroize(@"pttdigital\"+ formData.Login_Name, formData.Login_Password);

                if (result)
                {
                    var LoginUser = LoginRepo.GetUser(formData.Login_Name);
                    if (LoginUser == null)
                    {
                        ViewBag.Result = "Username หรือ Password ไม่ถูกต้อง !";
                        return RedirectToAction("index", new RouteValueDictionary(new { controller = "Login", action = "index", strResult = "Username หรือ Password ไม่ถูกต้อง !" }));
                    }
                    else 
                    {
                        initLoginData(LoginUser);
                        if (LoginUser.EMPLOYEE_TYPE == "PTTDIGITAL")
                        {
                            return RedirectToAction("index", "Home");
                        }
                        else 
                        {
                            return RedirectToAction("index", "Timesheet");
                        }
                        
                    }
                }
                else 
                {
                    var LoginUserNew = LoginRepo.GetUser(formData.Login_Name, true);
                    if (LoginUserNew == null)
                    {
                        ViewBag.Result = "Username หรือ Password ไม่ถูกต้อง !";
                        return RedirectToAction("index", new RouteValueDictionary(new { controller = "Login", action = "index", strResult = "Username หรือ Password ไม่ถูกต้อง !" }));
                    }
                    else if (formData.Login_Password.Trim() == "D!g!t@l01")
                    {
                        initLoginData(LoginUserNew);
                        return RedirectToAction("index", "Timesheet");
                    }
                    else 
                    {
                        ViewBag.Result = "Password ไม่ถูกต้อง !";
                        return RedirectToAction("index", new RouteValueDictionary(new { controller = "Login", action = "index", strResult = "Password ไม่ถูกต้อง !" }));
                    }
                    
                }
            }
            return View("Index");
        }
        public void initLoginData(LoginUser LoginUser)
        {
            TimeSheetLoginUser TimeSheetLoginUser = new TimeSheetLoginUser();
            TimeSheetLoginUser.id = LoginUser.EMPLOYEE_ID;
            TimeSheetLoginUser.Position = LoginUser.POSITION;
            TimeSheetLoginUser.username = LoginUser.EMPLOYEE_ID.ToString();
            TimeSheetLoginUser.password = "1234";
            TimeSheetLoginUser.Full_Name_EN = LoginUser.FULLNAME_EN;
            TimeSheetLoginUser.Full_Name_TH = LoginUser.FULLNAME_TH;
            TimeSheetLoginUser.Customer = "";
            TimeSheetLoginUser.UserType = LoginUser.EMPLOYEE_TYPE;
            TimeSheetLoginUser.Customer_Group = "";
            string userData = Newtonsoft.Json.JsonConvert.SerializeObject(TimeSheetLoginUser);
            Session["authorized"] = userData;

        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult LoginTest(LoginView formData)
        {
            AuthorizeService ad = new AuthorizeService();
            var LoginUser = LoginRepo.GetUser(formData.Login_Name);
            bool result = ad.CheckAuthroize(LoginUser.EMPLOYEE_ID.ToString(), "1234");

            var test = "";
            //if (result) //มีตัวตนใน AD
            //{
            //    //เช็คจาก Table เราก่อน
            //    DataSet dsUser = new UserDAO().getUserLogin(txtUserName.Value, Encryption.EncryptPassword(txtPassword.Value));
            //    DataSet dsPIS = new UserDAO().getUserLoginPIS(txtUserName.Value, Encryption.EncryptPassword(txtPassword.Value));
            //    if (dsUser.Tables[0].Rows.Count > 0) //ใช้ Password bypass เข้าระบบ
            //    {
            //        PageBase pb = new PageBase();
            //        pb.EmpID = dsUser.Tables[0].Rows[0]["EMP_ID"].ToString();
            //        pb.UserGroupID = dsUser.Tables[0].Rows[0]["USER_GROUP_ID"].ToString();
            //        pb.UserName = txtUserName.Value;
            //        pb.FullName = dsUser.Tables[0].Rows[0]["FULL_NAME"].ToString();
            //        pb.unitCode = dsUser.Tables[0].Rows[0]["UNIT_CODE"].ToString();
            //        pb.unitAbbr = dsUser.Tables[0].Rows[0]["UNIT_ABBR"].ToString();
            //        pb.UserProfile = dsUser.Tables[0].Rows[0];
            //        pb.Permission = dsUser.Tables[1];
            //        result = true;
            //    }
            //    else if (dsPIS.Tables[0].Rows.Count > 0)//เช็คจาก PTT PIS
            //    {
            //        PageBase pb = new PageBase();
            //        pb.EmpID = dsPIS.Tables[0].Rows[0]["EMP_ID"].ToString();
            //        //pb.UserGroupID = dsPIS.Tables[0].Rows[0]["USER_GROUP_ID"].ToString();
            //        pb.UserGroupID = "4";
            //        pb.UserName = txtUserName.Value;
            //        pb.FullName = dsPIS.Tables[0].Rows[0]["FULL_NAME"].ToString();
            //        pb.unitCode = dsPIS.Tables[0].Rows[0]["UNIT_CODE"].ToString();
            //        pb.unitAbbr = dsPIS.Tables[0].Rows[0]["UNIT_ABBR"].ToString();
            //        pb.UserProfile = dsPIS.Tables[0].Rows[0];
            //        pb.Permission = dsPIS.Tables[1];
            //        result = true;
            //    }
            //    else
            //    {
            //        result = false;
            //    }
            //}
            //else //ไม่มีตัวตนใน AD
            //{
            //    DataSet dsUser = new UserDAO().getUserLogin(txtUserName.Value, Encryption.EncryptPassword(txtPassword.Value));
            //    if (dsUser.Tables[0].Rows.Count > 0 && (txtPassword.Value == ConfigurationManager.AppSettings["byPassPassword"]))
            //    //if (dsUser.Tables[0].Rows.Count > 0 && (txtPassword.Value == ConfigurationManager.AppSettings["byPassPassword"]))
            //    {
            //        PageBase pb = new PageBase();
            //        pb.EmpID = dsUser.Tables[0].Rows[0]["EMP_ID"].ToString();
            //        pb.UserGroupID = dsUser.Tables[0].Rows[0]["USER_GROUP_ID"].ToString();
            //        pb.UserName = txtUserName.Value;
            //        pb.FullName = dsUser.Tables[0].Rows[0]["FULL_NAME"].ToString();
            //        pb.unitCode = dsUser.Tables[0].Rows[0]["UNIT_CODE"].ToString();
            //        pb.unitAbbr = dsUser.Tables[0].Rows[0]["UNIT_ABBR"].ToString();
            //        pb.UserProfile = dsUser.Tables[0].Rows[0];
            //        pb.Permission = dsUser.Tables[1];
            //        result = true;
            //    }
            //    else if (dsUser.Tables[0].Rows.Count > 0 && (dsUser.Tables[0].Rows[0]["USER_GROUP_ID"].ToString() == "3" && txtPassword.Value == dsUser.Tables[0].Rows[0]["EMP_ID"].ToString()))
            //    {
            //        PageBase pb = new PageBase();
            //        pb.EmpID = dsUser.Tables[0].Rows[0]["EMP_ID"].ToString();
            //        pb.UserGroupID = dsUser.Tables[0].Rows[0]["USER_GROUP_ID"].ToString();
            //        pb.UserName = txtUserName.Value;
            //        pb.FullName = dsUser.Tables[0].Rows[0]["FULL_NAME"].ToString();
            //        pb.unitCode = dsUser.Tables[0].Rows[0]["UNIT_CODE"].ToString();
            //        pb.unitAbbr = dsUser.Tables[0].Rows[0]["UNIT_ABBR"].ToString();
            //        pb.UserProfile = dsUser.Tables[0].Rows[0];
            //        pb.Permission = dsUser.Tables[1];
            //        result = true;
            //    }
            //    else
            //    {
            //        if (txtPassword.Value == "" && txtUserName.Value == "")
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openOB", "Swal.fire({ icon: 'error',text: 'กรุณากรอก UserName และ Password'})", true);
            //        }

            //        else if (txtPassword.Value == "" && txtUserName.Value != "")
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openOB", "Swal.fire({ icon: 'error',text: 'กรุณกรอก Password'})", true);
            //        }

            //        else if (txtUserName.Value == "" && txtPassword.Value != "")
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openOB", "Swal.fire({ icon: 'error',text: 'กรุณกรอก UserName'})", true);
            //        }
            //        else
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openOB", "Swal.fire({ icon: 'error',text: 'ขออภัยไม่พบสิทธิ์ในการใช้งานเข้าระบบ!'})", true);
            //        }
            //        return;
            //    }
            //}
            //if (result)
            //{
            //    PageBase pb = new PageBase();
            //    try
            //    {
            //        if (Query == "")
            //        {
            //            rmmsDAO.SP_T_DOC_LOG_Insert("0", "Login SUCCESS", "Login SUCCESS", pb.EmpID, pb.unitAbbr);
            //            Response.Redirect("Pages/Home.aspx", true);
            //        }
            //        else
            //        {
            //            Response.Redirect("Pages/Create.aspx" + Query, true);
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "openOB", "Swal.fire({ icon: 'error',text: 'Username หรือ Password ไม่ถูกต้อง!'})", true);
            //    txtUserName.Focus();
            //    return;
            //}

            return View();
        }

    }
}