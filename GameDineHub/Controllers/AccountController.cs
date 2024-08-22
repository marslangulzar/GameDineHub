using System;
using System.Web.Mvc;
using BusinessServices.Account;
using System.Web;
using BusinessEntities.Common;
using BusinessEntities.Enums;
using System.Linq;
using System.Web.Script.Serialization;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using GameDineHub.Model;
using System.Configuration;
using BusinessEntities.Entities.Account;
using BusinessEntities.Entities;
using System.Collections.Generic;

namespace GameDineHub.Controllers
{
    public class AccountController : BaseController
    {

        #region Services
        private readonly IAccountServices _accountServices;
        private readonly ISystemSettingServices _systemSettingServices;
        #endregion

        #region Constructor
        /// <summary>
        /// Public constructor.
        /// </summary>
        public AccountController()
        {
            _accountServices = new AccountServices();
            _systemSettingServices = new SystemSettingServices();
        }
        #endregion

        #region LogIn/Logout
        // GET: /account/login
        /// <summary>
        /// Logins the specified return URL.
        /// </summary>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User == null || !User.Identity.IsAuthenticated)
            {
                ViewBag.ReturnUrl = returnUrl;
                HttpContext.Response.AddHeader("LOGIN_SCREEN_Ready", "1");
                return View();
            }
            else
            {

                return RedirectToAction("Index", "Team");
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(ViewModels.AccountLoginModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid && LoginUser(model) > 0)
                {
                    return RedirectToAction("Manage", "General");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attempt. Username or password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Login attempt not successful!!! Please try again...\n ");
            }
            return View(model);
        }


        private int LoginUser(ViewModels.AccountLoginModel model)
        {
            try
            {
                int roleId = 0;
                //check user should be activated otherwise through deactivate account exception
                if (_accountServices.AuthenticateUser(model.Email, model.Password))
                {
                    var userInfo = _accountServices.GetUserByEmail(model.Email);

                    var currentRolesList = _accountServices.GetUserCurrentRoles(userInfo.ID);

                    //===Set LastLogin Date & Time
                    var datetime = DateTime.Now;

                    // Update UserLogin and Logout Datetime for User
                    //UserLoginHistoryVm loginHistoryObj = new UserLoginHistoryVm();
                    //var LoginUserInfoData = SecurityClaims.GetCurrentUserInfo(User.Identity as ClaimsIdentity);
                    //loginHistoryObj.fk_UserId = LoginUserInfoData.UserId;
                    //loginHistoryObj.ID = LoginUserInfoData.UserLoginHistoryID;
                    //loginHistoryObj.LoginDateTime = datetime;
                    //loginHistoryObj.LogoutDateTime = null;
                    //string LogType = "";
                    //int UserLoginHistoryID = _generalService.setUserLoginTime(loginHistoryObj, LogType);

                    var userInfoData = new UserInfoVM
                    {
                        UserId = userInfo.ID,
                        Email = userInfo.Email,
                        FirstName = userInfo.FirstName,
                        LastName = userInfo.LastName,
                        //FullName = userInfo.FirstName + ' ' + userInfo.LastName,
                        LastLogin = userInfo.LastLogin,
                        UserRoles = currentRolesList,
                        LoginDateTime = datetime
                    };
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    string serializeUserData = serializer.Serialize(userInfoData);
                    //Logged in successfully
                    var ident = new ClaimsIdentity(
                    new[] { 
                                      // adding following 2 claim just for supporting default antiforgery provider
                                      new Claim(ClaimTypes.NameIdentifier, userInfo.ID.ToString()),
                                      new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                                      new Claim(ClaimTypes.Name, userInfo.LastName??""),
                                      new Claim(ClaimTypes.GivenName, userInfo.FirstName??""),
                                      new Claim(ClaimTypes.Surname, userInfo.LastName??""),
                                      new Claim(ClaimTypes.Email, userInfo.Email),
                                      new Claim(ClaimTypes.UserData, serializeUserData)

                    }, DefaultAuthenticationTypes.ApplicationCookie);
                    // Add roles list into Claim
                    if (userInfo != null && currentRolesList.Count > 0)
                    {
                        roleId = currentRolesList.First().RoleID;
                        foreach (var role in currentRolesList)
                        {
                            ident.AddClaim(new Claim(ClaimTypes.Role, role.RoleName));  // For now i have added admin, you will fetch all roles from db and add them as a claim in order to user Built in Authorization feature
                        }
                    }
                    HttpContext.GetOwinContext().Authentication.SignIn(new Microsoft.Owin.Security.AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddDays(1) }, ident);


                    return roleId;
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception e)
            {
                return 0;
            }

        }
        /// <summary>
        /// Logouts this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [Authorize]
        public ActionResult Logout()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    HttpContext.GetOwinContext().Authentication.SignOut();

                    // Update UserLogin and Logout Datetime for User
                    //UserLoginHistoryVm loginHistoryObj = new UserLoginHistoryVm();
                    //var LoginUserInfoData = SecurityClaims.GetCurrentUserInfo(User.Identity as ClaimsIdentity);
                    //loginHistoryObj.fk_UserId = LoginUserInfoData.UserId;
                    //loginHistoryObj.ID = LoginUserInfoData.UserLoginHistoryID;
                    //loginHistoryObj.LoginDateTime = (DateTime)LoginUserInfoData.LoginDateTime;
                    //loginHistoryObj.LogoutDateTime = DateTime.Now;
                    //string LogType = "Update";
                    //int UserLoginHistoryID = _generalService.setUserLoginTime(loginHistoryObj, LogType);
                }
            }
            catch (Exception ex)
            {
            }

            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region Forgot Password
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            if (User == null || !User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        /// Reset password page.
        /// </summary>
        /// <returns>ActionResult.</returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string id)
        {
            if (User == null || !User.Identity.IsAuthenticated)
            {

                string userFeedback = "Your reset password link is invalid.";
                string userName = "";
                if (!string.IsNullOrWhiteSpace(id))
                {
                    int urlExpiryTime = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordURLExpiryTime"]);
                    // validate change password request token
                    string urlValidationStatus = _accountServices.validateChangePasswordURL(id, urlExpiryTime);
                    switch (urlValidationStatus)
                    {
                        case "Invalid":
                            userFeedback = "Your reset password link is invalid.";
                            break;
                        case "Expired":
                            userFeedback = "Your reset password link has been expired. Please request again if you still want to reset your password.";
                            break;
                        default:
                            userFeedback = "Please review the safety tips and then provide your new password below.";
                            userName = urlValidationStatus;
                            break;
                    }
                }


                ViewBag.PageCaption = "Reset Password";
                ViewBag.UserFeedbackMessage = userFeedback;
                ViewBag.UserName = userName;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        ///// <summary>
        ///// Changes the password.
        ///// </summary>
        ///// <param name="changePasswordRequestVM">The changePasswordRequestVM.</param>
        ///// <returns>ActionResult.</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPassword(ChangePasswordRequestVM changePasswordRequestVM)
        {
            string userFeedbackMessage = "Sorry, Currently system is unable to process your request.";
            bool IsSuccess = true;
            try
            {
                string loginUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/');
                changePasswordRequestVM.UrlExpiryTime = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordURLExpiryTime"]);
                string changePasswordStatus = _accountServices.ResetPassword(changePasswordRequestVM);
                switch (changePasswordStatus)
                {
                    case "Password changed successfully.":
                        loginUrl = loginUrl + "/Account/Login";
                        userFeedbackMessage = "You password has been reset successfully. Please <a href='" + loginUrl + "'>login</a> to the system using your password you have just set.";
                        IsSuccess = true;
                        break;
                    case "URL has been expired.":
                        loginUrl = loginUrl + "/Account/ForgotPassword";
                        userFeedbackMessage = "Your reset password link has been expired. Please request again if you still want to reset your password. Go back to <a href='" + loginUrl + "'>login</a>.";
                        IsSuccess = false;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                //Log4Net.WriteException(ex);
                IsSuccess = false;
            }
            return Json(new { userFeedbackMessage, IsSuccess }, JsonRequestBehavior.AllowGet);
        }
        ///// <summary>
        ///// Changes the password.
        ///// </summary>
        ///// <param name="changePasswordRequestVM">The changePasswordRequestVM.</param>
        ///// <returns>ActionResult.</returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ChangePassword(ChangePasswordRequestVM changePasswordRequestVM)
        {

            string userFeedbackMessage = "Sorry, Currently system is unable to process your request.";

            try
            {
                var user = _currentUserInfo;
                changePasswordRequestVM.UserName = user.Email;
                //string loginUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/');
                changePasswordRequestVM.UrlExpiryTime = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordURLExpiryTime"]);
                userFeedbackMessage = _accountServices.ChangePassword(changePasswordRequestVM);


            }
            catch (Exception ex)
            {
                //Log4Net.WriteException(ex);

            }
            return Json(new { userFeedbackMessage }, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public ActionResult SendResetPasswordEmail(string email)
        {
            try
            {
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";
                string changePasswordToken = Guid.NewGuid().ToString();
                bool result = false;
                if (_accountServices.updateChangePasswordRequestHash(email, changePasswordToken))
                {
                    var appSettings = _systemSettingServices.GetAppSetting();
                    result = GeneralUtilities.SendResetPasswordEmail(_accountServices.GetUserByEmail(email), baseUrl, appSettings);


                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(GeneralUtilities.JsonErrorMessage(ex), JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Manage Users
        [Authorize]
        /// <summary>
        /// Manages the users.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult ManageUsers()
        {
            ViewBag.UserRolesList = _accountServices.GetAllRoles();
            var usersList = _accountServices.GetAllUsers();
            return View("ManageUsers", usersList);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UpsertUser(UserInfoVM userDataToAdd)
        {
            try
            {
                bool isAddCase = userDataToAdd.UserId > 0 ? false : true;
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/') + "/";

                if (userDataToAdd.UserId == 0 && _accountServices.IsEmailExist(userDataToAdd.Email))
                {
                    return Json(GeneralUtilities.JsonErrorMessage(new Exception("Email is already exist. Please enter another email.")), JsonRequestBehavior.AllowGet);
                }
                userDataToAdd.Email = userDataToAdd.Email.Trim();
                GeneralUtilities.SetBaseValues(userDataToAdd, _currentUserInfo, userDataToAdd.UserId);
                userDataToAdd.MobileNo = userDataToAdd.MobileNo?.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Trim();
                userDataToAdd.PhoneNo = userDataToAdd.PhoneNo?.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "").Trim();
                var userID = _accountServices.UpsertUser(userDataToAdd);
                if (userID > 0)
                {
                    if (isAddCase)
                    {
                        if (_accountServices.updateChangePasswordRequestHash(userDataToAdd.Email, Guid.NewGuid().ToString()))
                        {
                            var systemSettings = _systemSettingServices.GetAppSetting();
                            var status = GeneralUtilities.SendSignUpEmail(_accountServices.GetUserByEmail(userDataToAdd.Email), baseUrl, systemSettings);

                        }
                    }
                    return Json(userID, JsonRequestBehavior.AllowGet);
                }
                return Json(GeneralUtilities.JsonErrorMessage(new Exception("User not created/updated...")), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(GeneralUtilities.JsonErrorMessage(ex), JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Actives the in active user.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult ActiveInActiveUser(int userID, bool active)
        {

            try
            {
                var resopose = _accountServices.ActiveInActiveUser(userID, active);
                return Json(resopose, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(GeneralUtilities.JsonErrorMessage(ex), JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region SystemSettings
        // GET: Admin Setting Page
        [Authorize]
        public ActionResult SystemSetting()
        {
            return View(_systemSettingServices.GetAppSetting());
        }

        [HttpPost]
        [Authorize]
        public ActionResult UpdateAppSetting(List<AppSettingVm> appSettings)
        {
            var result = _systemSettingServices.UpdateAppSetting(appSettings);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}