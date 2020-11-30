using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Timesheet.Web.Models
{
    public class LoginModel
    {
        public int ID { get; set; }
        public string Login_Name { get; set; }
        public string Login_Password { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Department_Code { get; set; }
        public string Position_Code { get; set; }
        public string Phone_No { get; set; }
        public string Mobile_No { get; set; }
        public string Email { get; set; }
        public string Locked { get; set; }
        public string Login_Status { get; set; }
        public string Active_Status { get; set; }

    }

    public class LoginView
    {
        public string Login_Name { get; set; }
        [DataType(DataType.Password)]
        public string Login_Password { get; set; }
        public bool RememberMe { get; set; }
        public string returnUrl { get; set; }
    }

    public class TimeSheetLoginUser
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string UserType { get; set; }
        public string Customer { get; set; }
        public string Customer_Group { get; set; }
        public string Full_Name_EN { get; set; }
        public string Full_Name_TH { get; set; }

    }

    public class LoginUser
    {
        public int EMPLOYEE_ID { get; set; }
        public int? EMPLOYEE_NO { get; set; }
        public string FULLNAME_TH { get; set; }
        public string FULLNAME_EN { get; set; }
        public string NICKNAME { get; set; }
        public DateTime? BIRTH_DATE { get; set; }
        public string ZODIAC { get; set; }
        public string POSITION { get; set; }
        public string POSITION_LEVEL { get; set; }
        public string COMPANY { get; set; }
        public string MOBILE_NUMBER { get; set; }
        public string MAIL_PTT_DIGITAL { get; set; }
        public string MAIL_OTHER { get; set; }
        public string LINE_ID { get; set; }
        public string ADDRESS { get; set; }
        public DateTime? INTERVIEW_DATE { get; set; }
        public DateTime? START_WORK_DATE { get; set; }
        public string STATUS { get; set; }
    }

    public class ResetPassword
    {
        public string userName { get; set; }
    }

}