using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timesheet.Web.EF;
using Timesheet.Web.Models;

namespace Timesheet.Web.Repositories
{
    public class LoginRepo
    {
        public static LoginUser GetUser(string Login_Name)
        {
            try
            {
                using (DB_TIMESHEETEntities db = new DB_TIMESHEETEntities())
                {
                    LoginUser loginUser = db.TB_EMPLOYEE.Select(t => new LoginUser
                    {
                        EMPLOYEE_ID = t.EMPLOYEE_ID,
                        FULLNAME_TH = t.FULLNAME_TH,
                        FULLNAME_EN = t.FULLNAME_EN,
                        NICKNAME = t.NICKNAME,
                        BIRTH_DATE = t.BIRTH_DATE,
                        ZODIAC = t.ZODIAC,
                        POSITION = t.POSITION,
                        POSITION_LEVEL = t.POSITION_LEVEL,
                        COMPANY = t.COMPANY,
                        MOBILE_NUMBER = t.MOBILE_NUMBER,
                        MAIL_PTT_DIGITAL = t.MAIL_PTT_DIGITAL,
                        MAIL_OTHER = t.MAIL_OTHER,
                        LINE_ID = t.LINE_ID,
                        ADDRESS = t.ADDRESS,
                        INTERVIEW_DATE = t.INTERVIEW_DATE,
                        START_WORK_DATE = t.START_WORK_DATE,
                        STATUS = t.STATUS

                    }).FirstOrDefault(p => p.MAIL_PTT_DIGITAL.ToLower().Contains(Login_Name.ToLower()));
                    return loginUser;
                }
            }
            catch (Exception ex)
            {
                return new LoginUser();
            }
        }

        public static TimeSheetLoginUser GetOwnerUser()
        {
            try
            {
                if (HttpContext.Current.Session["authorized"] != null)
                {
                    var data = HttpContext.Current.Session["authorized"] as string;
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<TimeSheetLoginUser>(data);
                }
                else
                {
                    return new TimeSheetLoginUser() { id = -1 };
                }
            }
            catch { return new TimeSheetLoginUser() { id = -1 }; }
        }
        //private int rep = 0;
        public static string GenerateCheckCode(int codeCount)
        {
            int rep = 0;
            string str = string.Empty;
            long num2 = DateTime.Now.Ticks + rep;
            rep++;
            Random random = new Random(((int)(((ulong)num2) & 0xffffffffL)) | ((int)(num2 >> rep)));
            for (int i = 0; i < codeCount; i++)
            {
                char ch;
                int num = random.Next();
                if ((num % 2) == 0)
                {
                    ch = (char)(0x30 + ((ushort)(num % 10)));
                }
                else
                {
                    ch = (char)(0x41 + ((ushort)(num % 0x1a)));
                }
                str = str + ch.ToString();
            }
            return str;
        }
    }
}