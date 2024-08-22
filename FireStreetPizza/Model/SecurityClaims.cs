using System;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Newtonsoft.Json;
using BusinessServices.Account;
using BusinessEntities.Entities.Account;
using BusinessEntities.Entities.Game;

namespace FireStreetPizza.Model
{
    /// <summary>
    /// Authenticates/authorizes the current user 
    /// </summary>
    public static class SecurityClaims
    {
        /// <summary>
        /// Gets the current user information.
        /// </summary>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <returns>UserInfoVm.</returns>
        public static UserInfoVM GetCurrentUserInfo(this ClaimsIdentity claimsIdentity)
        {
            UserInfoVM bsObj = null;
            var UserDataString = claimsIdentity.Claims.FirstOrDefault(x => x.Type.ToLower() == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata".ToLower());
            var UserDataValue = UserDataString == null ? string.Empty : UserDataString.Value;

            if (UserDataValue != null)
            {
                bsObj = JsonConvert.DeserializeObject<UserInfoVM>(UserDataValue);
                {
                    if (bsObj != null)
                        bsObj.IsAdmin = GetCurrentUserIsSystemAdmin(claimsIdentity);
                }
            }
            return bsObj;
        }

        /// <summary>
        /// Gets Logged in user detail.
        /// </summary>
        /// <returns>UserInfoVm.</returns>
        public static UserInfoVM GetLoggedInUserDetail()
        {
            var claimsIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            UserInfoVM userInforVM = null;
            var UserDataString = claimsIdentity.Claims.FirstOrDefault(x => x.Type.ToLower() == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata".ToLower());
            var UserDataValue = UserDataString == null ? string.Empty : UserDataString.Value;

            if (UserDataValue != null)
            {
                userInforVM = JsonConvert.DeserializeObject<UserInfoVM>(UserDataValue);
                {
                    if (userInforVM != null)
                        userInforVM.IsAdmin = GetCurrentUserIsSystemAdmin(claimsIdentity);
                }
            }
            return userInforVM;
        }

        /// <summary>
        /// Gets Logged in team detail.
        /// </summary>
        /// <returns>TeamVM.</returns>
        public static TeamVM GetTeamDetail()
        {
            var claimsIdentity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            TeamVM teamVM = null;
            var UserDataString = claimsIdentity.Claims.FirstOrDefault(x => x.Type.ToLower() == "http://schemas.microsoft.com/ws/2008/06/identity/claims/userdata".ToLower());
            var UserDataValue = UserDataString == null ? string.Empty : UserDataString.Value;

            if (UserDataValue != null)
            {
                teamVM = JsonConvert.DeserializeObject<TeamVM>(UserDataValue);
            }
            return teamVM;
        }
        /// <summary>
        /// Gets the current user is system admin.
        /// </summary>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        private static bool GetCurrentUserIsSystemAdmin(ClaimsIdentity claimsIdentity)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(r => r.Type.ToLower() == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/system".ToLower());
            var val = claim == null ? string.Empty : claim.Value;
            try
            {
                return Convert.ToBoolean(val);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Gets the current user identifier.
        /// </summary>
        /// <param name="claimsIdentity">The claims identity.</param>
        /// <returns>System.Int32.</returns>
        private static int GetCurrentUserId(ClaimsIdentity claimsIdentity)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(r => r.Type.ToLower() == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/NameIdentifier".ToLower());
            var val = claim == null ? string.Empty : claim.Value;
            try
            {
                return Convert.ToInt16(val);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}