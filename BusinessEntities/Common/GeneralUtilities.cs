using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using BusinessEntities.Entities;
using BusinessEntities.Entities.Account;
using BusinessEntities.Entities.Common;
using BusinessEntities.Enums;

namespace BusinessEntities.Common
{
    public static class GeneralUtilities
    {
        
        #region Cached Objects
        public static List<AppSettingVm> AppSettings = new List<AppSettingVm>();
        #endregion

        #region Datatable Page Number
        /// <summary>
        /// Gets the page number.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>System.Int32.</returns>
        public static int getPageNumber(int? pageNumber)
        {
            int currentPage;
            if (pageNumber == null || pageNumber == 0)
            {
                currentPage = 1;
            }
            else
            {
                currentPage = (int)pageNumber;
            }
            return currentPage;
        }
        #endregion

        #region Json Error Response
        /// <summary>
        /// Jsons the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>System.Object.</returns>
        public static object JsonErrorMessage(Exception ex)
        {
            return new
            {
                error = true,
                msg = ex.Message
            };
        }
        #endregion

        #region Date Range Filter 
        public static void SetDateRangeFilterVmByFilterEnumValue(DateRangeFilterVM dateRangeFilterVM, DateRangeFilter dateRangeFilter)
        {
            var DateTimeNow = DateTime.Now.Date;
            switch (dateRangeFilter)
            {
                case DateRangeFilter.Today:
                    dateRangeFilterVM.DateFrom = DateTimeNow;
                    dateRangeFilterVM.DateTo = DateTimeNow;
                    dateRangeFilterVM.DateRangeFilterText = "Today";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.Today;
                    break;

                case DateRangeFilter.Yesterday:
                    dateRangeFilterVM.DateFrom = DateTimeNow.AddDays(-1);
                    dateRangeFilterVM.DateTo = DateTimeNow.AddDays(-1);
                    dateRangeFilterVM.DateRangeFilterText = "Yesterday";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.Yesterday;
                    break;
                case DateRangeFilter.Last2Days:
                    dateRangeFilterVM.DateFrom = DateTimeNow.AddDays(-2);
                    dateRangeFilterVM.DateTo = DateTimeNow;
                    dateRangeFilterVM.DateRangeFilterText = "Last 2 Days";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.Last2Days;
                    break;

                case DateRangeFilter.Last7Days:
                    dateRangeFilterVM.DateFrom = DateTimeNow.AddDays(-7);
                    dateRangeFilterVM.DateTo = DateTimeNow;
                    dateRangeFilterVM.DateRangeFilterText = "Last 7 Days";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.Last7Days;
                    break;
                case DateRangeFilter.Last14Days:
                    dateRangeFilterVM.DateFrom = DateTimeNow.AddDays(-14);
                    dateRangeFilterVM.DateTo = DateTimeNow;
                    dateRangeFilterVM.DateRangeFilterText = "Last 14 Days";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.Last14Days;
                    break;

                case DateRangeFilter.Last30Days:
                    dateRangeFilterVM.DateFrom = DateTimeNow.AddDays(-30);
                    dateRangeFilterVM.DateTo = DateTimeNow;
                    dateRangeFilterVM.DateRangeFilterText = "Last 30 Days";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.Last30Days;
                    break;
                case DateRangeFilter.Last60Days:
                    dateRangeFilterVM.DateFrom = DateTimeNow.AddDays(-60);
                    dateRangeFilterVM.DateTo = DateTimeNow;
                    dateRangeFilterVM.DateRangeFilterText = "Last 60 Days";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.Last60Days;
                    break;
                case DateRangeFilter.Last90Days:
                    dateRangeFilterVM.DateFrom = DateTimeNow.AddDays(-90);
                    dateRangeFilterVM.DateTo = DateTimeNow;
                    dateRangeFilterVM.DateRangeFilterText = "Last 90 Days";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.Last90Days;
                    break;

                case DateRangeFilter.MonthToDate:
                    dateRangeFilterVM.DateFrom = DateTimeHelpers.GetStartOfMonthToDate();
                    dateRangeFilterVM.DateTo = DateTimeHelpers.GetEndOfMonthToDate();
                    dateRangeFilterVM.DateRangeFilterText = "Month To Date";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.MonthToDate;
                    break;

                case DateRangeFilter.PreviousMonth:
                    dateRangeFilterVM.DateFrom = DateTimeHelpers.GetStartOfLastMonth();
                    dateRangeFilterVM.DateTo = DateTimeHelpers.GetEndOfLastMonth();
                    dateRangeFilterVM.DateRangeFilterText = "Previous Month";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.PreviousMonth;
                    break;

                case DateRangeFilter.CurrentQuarter:
                    dateRangeFilterVM.DateFrom = DateTimeHelpers.GetStartOfCurrentQuarter();
                    dateRangeFilterVM.DateTo = DateTimeHelpers.GetEndOfCurrentQuarter();
                    dateRangeFilterVM.DateRangeFilterText = "Current Quarter";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.CurrentQuarter;
                    break;

                case DateRangeFilter.PreviousQuarter:
                    dateRangeFilterVM.DateFrom = DateTimeHelpers.GetStartOfLastQuarter();
                    dateRangeFilterVM.DateTo = DateTimeHelpers.GetEndOfLastQuarter();
                    dateRangeFilterVM.DateRangeFilterText = "Previous Quarter";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.PreviousQuarter;

                    break;

                case DateRangeFilter.YearToDate:
                    dateRangeFilterVM.DateFrom = DateTimeHelpers.GetStartOfCurrentYear();
                    dateRangeFilterVM.DateTo = DateTimeHelpers.GetEndOfYearToDate();
                    dateRangeFilterVM.DateRangeFilterText = "Year To Date";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.YearToDate;
                    break;

                case DateRangeFilter.PreviousYear:
                    dateRangeFilterVM.DateFrom = DateTimeHelpers.GetStartOfLastYear();
                    dateRangeFilterVM.DateTo = DateTimeHelpers.GetEndOfLastYear();
                    dateRangeFilterVM.DateRangeFilterText = "Previous Year";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.PreviousYear;
                    break;

                case DateRangeFilter.CustomDateRange:
                    //DateFrom and DateTo must have values submitted from UI in this case
                    dateRangeFilterVM.DateFrom = dateRangeFilterVM.DateFrom;
                    dateRangeFilterVM.DateTo = dateRangeFilterVM.DateTo;
                    dateRangeFilterVM.DateRangeFilterText = "Custom Date Range";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.CustomDateRange;
                    break;

                default:
                    dateRangeFilterVM.DateFrom = new DateTime().AddYears(1899);
                    dateRangeFilterVM.DateTo = DateTimeNow;
                    dateRangeFilterVM.DateRangeFilterText = "All";
                    dateRangeFilterVM.CurrentDateRangeFilter = DateRangeFilter.All;
                    break;
            }
        }
        #endregion

        #region Common
        public static bool SendResetPasswordEmail(UserVM userDetail, string baseUrl, List<AppSettingVm> appSettings)
        {
            string linkFontStyle = "color:#aaaaaa;font-size:16px;font-family:Helvetica Neue,Helvetica,Lucida Grande,tahoma,verdana,arial,sans-serif;line-height:21px;color:#3b5998";
            string body = "<div id=\":a3\" class=\"a3s aXjCH \"><b><b><font style=\"font-size: 16px; font-family: &quot;Helvetica Neue&quot;, Helvetica, &quot;Lucida Grande&quot;, tahoma, verdana, arial, sans-serif; line-height: 21px; color: rgb(20, 24, 35);\">Hi&nbsp;</font> {FirstName}  {LastName}</b> </b><br><br><font style=\"color:#aaaaaa;font-size:16px;font-family:Helvetica Neue,Helvetica,Lucida Grande,tahoma,verdana,arial,sans-serif;line-height:21px;color:#141823\">You recently requested to reset your Game Dine Hub Portal password.</font><br><br> {URL} <br><br><font style=\"color:#aaaaaa;font-size:16px;font-family:Helvetica Neue,Helvetica,Lucida Grande,tahoma,verdana,arial,sans-serif;line-height:21px;color:#141823\">If you did not make this request and deem this to be malicious, please alert your system administrator immediately.</font><br><br><font style=\"color:#aaaaaa;font-size:11px;font-family:Helvetica Neue,Helvetica,Lucida Grande,tahoma,verdana,arial,sans-serif;line-height:16px;color:#aaaaaa\">This message was sent to&nbsp;<span style=\"color: rgb(0, 0, 0); font-family: Montserrat, &quot;Helvetica Neue&quot;, Helvetica, Arial; font-size: 13px; letter-spacing: 0.1px;\">{FirstName} {LastName}<span style=\"font-weight: bolder;\">&nbsp;</span></span>by the Game Dine Hub Team.</font><div class=\"yj6qo\"></div><div class=\"adL\"><br></div></div>";
            string reseturl = baseUrl + "/Account/ResetPassword?id=" + userDetail.PasswordRequestHash;
            string emailTemplateBody = body.Replace("{FirstName}", userDetail.FirstName).Replace("{LastName}", userDetail.LastName).Replace("{URL}", "<a href='" + reseturl + "'><font style='" + linkFontStyle + "'>Click here to reset your password</font></a><br/><br/>");
            return sendEmail(emailTemplateBody, userDetail.Email, "Reset Password | Game Dine Hub", appSettings, null, true);
        }
        public static bool sendEmail(string message, string to, string subject, List<AppSettingVm> appSettings, List<string> ccs = null, bool isbodyhtml = false, Attachment file = null)
        {
            try
            {
                //#if (!DEBUG)
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(appSettings.Where(x => x.Name == BusinessEntities.Enums.SystemSettingsVariables.FromEmail).FirstOrDefault().Value); //From Mail
                    mail.To.Add(new MailAddress(to));
                    if (ccs != null)
                    {
                        foreach (string cc in ccs)
                        {
                            mail.CC.Add(cc.ToString());
                        }
                    }
                    mail.IsBodyHtml = isbodyhtml ? true : false;

                    mail.Subject = subject;
                    mail.Body = message;
                    if (file != null)
                    {
                        mail.Attachments.Add(file);
                    }
                    //using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    using (SmtpClient smtp = new SmtpClient(appSettings.Where(x => x.Name == BusinessEntities.Enums.SystemSettingsVariables.SmtpClient).FirstOrDefault().Value, Convert.ToInt32(appSettings.Where(x => x.Name == BusinessEntities.Enums.SystemSettingsVariables.SmtpPort).FirstOrDefault().Value)))
                    {
                        smtp.Credentials = new NetworkCredential(appSettings.Where(x => x.Name == BusinessEntities.Enums.SystemSettingsVariables.SmtpUser).FirstOrDefault().Value, appSettings.Where(x => x.Name == BusinessEntities.Enums.SystemSettingsVariables.SmtpPassword).FirstOrDefault().Value);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
                //#endif
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool SendSignUpEmail(UserVM userDetail, string baseUrl, List<AppSettingVm> appSettings)
        {
            string linkFontStyle = "color:#aaaaaa;font-size:16px;font-family:Helvetica Neue,Helvetica,Lucida Grande,tahoma,verdana,arial,sans-serif;line-height:21px;color:#3b5998";
            string reseturl = baseUrl + "/Account/ResetPassword?id=" + userDetail.PasswordRequestHash;
            string body = "<div id=\":a3\" class=\"a3s aXjCH \"><b><b><font style=\"font-size: 16px; font-family: &quot;Helvetica Neue&quot;, Helvetica, &quot;Lucida Grande&quot;, tahoma, verdana, arial, sans-serif; line-height: 21px; color: rgb(20, 24, 35);\">Hi&nbsp;</font> {FirstName}  {LastName}</b> </b><br><br><font style=\"color:#aaaaaa;font-size:16px;font-family:Helvetica Neue,Helvetica,Lucida Grande,tahoma,verdana,arial,sans-serif;line-height:21px;color:#141823\">Your account is created. Please use below link to set your password.</font><br><br> {URL} <br><br><font style=\"color:#aaaaaa;font-size:16px;font-family:Helvetica Neue,Helvetica,Lucida Grande,tahoma,verdana,arial,sans-serif;line-height:21px;color:#141823\">If you did not make this request and deem this to be malicious, please alert your system administrator immediately.</font><br><br><font style=\"color:#aaaaaa;font-size:11px;font-family:Helvetica Neue,Helvetica,Lucida Grande,tahoma,verdana,arial,sans-serif;line-height:16px;color:#aaaaaa\">This message was sent to&nbsp;<span style=\"color: rgb(0, 0, 0); font-family: Montserrat, &quot;Helvetica Neue&quot;, Helvetica, Arial; font-size: 13px; letter-spacing: 0.1px;\">{FirstName} {LastName}<span style=\"font-weight: bolder;\">&nbsp;</span></span>by the Game Dine Hub Team.</font><div class=\"yj6qo\"></div><div class=\"adL\"><br></div></div>";
            string emailTemplateBody = body.Replace("{FirstName}", userDetail.FirstName).Replace("{LastName}", userDetail.LastName).Replace("{URL}", "<a href='" + reseturl + "'><font style='" + linkFontStyle + "'>Click here to set your password</font></a><br/><br/>"); ;
            return sendEmail(emailTemplateBody, userDetail.Email, "Sign Up | Game Dine Hub", appSettings, null, true);
        }
        
        #region Set Base Model
        public static void SetBaseValues(BaseModel model, UserInfoVM _currentUserInfo, int id = 0)
        {
            if (id == 0)
            {
                model.CreatedDate = DateTime.Now;
                if (_currentUserInfo != null)
                    model.CreatedBy = _currentUserInfo.UserId;
            }
            model.ModifiedDate = DateTime.Now;
            if (_currentUserInfo != null)
                model.ModifiedBy = _currentUserInfo.UserId;
        }
        #endregion

        #endregion

        #region User Data
        public static UserInfoVM GetCurrentUserInfo(this System.Security.Claims.ClaimsIdentity claimsIdentity)
        {
            UserInfoVM bsObj = null;
            var UserDataString = claimsIdentity.Claims.FirstOrDefault(x => x.Type.ToLower() == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata".ToLower());
            var UserDataValue = UserDataString == null ? string.Empty : UserDataString.Value;
            var username = claimsIdentity.Claims.FirstOrDefault(x => x.Type.ToLower() == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone".ToLower());
            //
            if (UserDataValue != null)
            {
                bsObj = Newtonsoft.Json.JsonConvert.DeserializeObject<UserInfoVM>(UserDataValue);
                if (username != null)
                    bsObj.FirstName = username.Value;
            }
            return bsObj;
        }
        #endregion

    }
}