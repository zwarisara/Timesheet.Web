using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timesheet.Web.Models;
using Timesheet.Web.Repositories;

namespace Timesheet.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index(String returnUrl)
        {
            return View(new Timesheet.Web.Models.LoginView() { returnUrl = returnUrl });
        }

        [HttpPost]
        public ActionResult Login(LoginView fromData)
        {
            if (ModelState.IsValid)
            {
                var LoginUser = Repositories.LoginRepo.GetUser(fromData.Login_Name);
                if (LoginUser == null)
                {
                    ModelState.AddModelError("", "User is no exist!(ชื่อผู้ใช้งานไม่มีในระบบ)");
                    ViewBag.Result = "UserName ไม่ถูกต้อง !";
                    return RedirectToAction("index");
                }
                else
                {
                    string LoginPassword = "1234";
                    var loginResult = false;
                    if (!String.IsNullOrEmpty(LoginPassword))
                        loginResult = EncryptionRepo.VerifyHash(fromData.Login_Password, "SHA256", LoginPassword);
                    if (fromData.Login_Password == LoginPassword) //loginResult
                    {
                        initLoginData(LoginUser);
                        if (String.IsNullOrWhiteSpace(fromData.returnUrl))
                            return RedirectToAction("index", "Home");
                        else
                            return Redirect(fromData.returnUrl);
                    }
                    else
                    {
                        ViewBag.Result = "PassWord ไม่ถูกต้อง !";
                        return RedirectToAction("index");
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
            TimeSheetLoginUser.UserType = "";
            TimeSheetLoginUser.Customer_Group = "";
            string userData = Newtonsoft.Json.JsonConvert.SerializeObject(TimeSheetLoginUser);
            Session["authorized"] = userData;

        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}