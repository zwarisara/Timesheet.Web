using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.Configuration;
using System.Reflection;

namespace Timesheet.Web
{
    public class AuthorizeService
    {
        public bool CheckAuthroize(string UserName, string Password)
        {
            DirectoryEntry entry = new DirectoryEntry(ConfigurationManager.AppSettings["ADPath"], UserName, Password, AuthenticationTypes.Secure);
            try
            {
                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(SAMAccountName=" + UserName + ")";
                search.PropertiesToLoad.Add("cn");
                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        private bool isPasswordExpired(string UserName)
        {
            DirectoryEntry de = new DirectoryEntry(ConfigurationManager.AppSettings["ADPath"]);
            DirectorySearcher search = new DirectorySearcher(de);
            search.Filter = "(SAMAccountName=" + UserName + ")";
            search.PropertiesToLoad.Add("maxPwdAge");
            search.PropertiesToLoad.Add("pwdLastSet");
            search.PropertiesToLoad.Add("userAccountControl");
            try
            {
                SearchResult result = search.FindOne();
                Int32 val1 = (Int32)result.Properties["userAccountControl"][0];
                if ((val1 & 65536) == 65536)
                {
                    return false;
                }
                else
                {
                    Int64 val = (Int64)result.Properties["pwdLastSet"][0];
                    DateTime d = DateTime.FromFileTime(val);
                    TimeSpan maxPwdAge = GetMaxPasswordAge();
                    DateTime exp = d.Add(maxPwdAge);
                    return exp < DateTime.Now;
                }
            }
            catch
            {
            }
            return false;
        }

        private static TimeSpan GetMaxPasswordAge()
        {
            using (System.DirectoryServices.ActiveDirectory.Domain d = System.DirectoryServices.ActiveDirectory.Domain.GetCurrentDomain())
            using (DirectoryEntry domain = d.GetDirectoryEntry())
            {
                DirectorySearcher ds = new DirectorySearcher(
                  domain,
                  "(objectClass=*)",
                  null,
                  SearchScope.Base
                  );
                SearchResult sr = ds.FindOne();
                TimeSpan maxPwdAge = TimeSpan.MinValue;
                if (sr.Properties.Contains("maxPwdAge"))
                    maxPwdAge = TimeSpan.FromTicks((long)sr.Properties["maxPwdAge"][0]);
                return maxPwdAge.Duration();
            }
        }
    }
}
