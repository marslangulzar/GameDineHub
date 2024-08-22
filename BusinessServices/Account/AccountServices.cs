using BusinessEntities.Common;
using BusinessEntities.Entities;
using BusinessEntities.Entities.Account;
using BusinessEntities.Enums;
using BusinessServices.Mapper;
using DataModel.Model;
using DataModel.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessServices.Account
{
    public class AccountServices : IAccountServices
    {
        private readonly UnitOfWork _unitOfWork;
        private int fk_UserAddressID;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public AccountServices()
        {
            _unitOfWork = new UnitOfWork();
        }

        #region Account
        public List<RoleVM> GetUserCurrentRoles(int userID)
        {
            List<RoleVM> result = new List<RoleVM>();
            try
            {
                result = _unitOfWork.RoleRepository.GetMany(x => x.IsActive && x.UserRoles.Any(y => y.fk_UserID == userID)).Select(x => new RoleVM()
                {
                    RoleID = x.RoleId,
                    RoleName = x.RoleName,
                    Selected = true,
                }).ToList();
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public UserVM GetUserByEmail(string email)
        {
            var user = _unitOfWork.UserRepository.GetFirstOrDefault(u => u.Email.ToUpper() == email.ToUpper());
            return Mapping.Mapper.Map<UserVM>(user);
        }
        #endregion

        #region ForGotPassWord Work
        public bool updateChangePasswordRequestHash(string userName, string changePasswordToken)
        {
            var userObjectToUpdate = _unitOfWork.UserRepository.GetSingle(x => x.Email == userName);
            if (userObjectToUpdate != null)
            {
                userObjectToUpdate.PasswordRequestHash = changePasswordToken;
                userObjectToUpdate.PasswordRequestDate = DateTime.Now;

                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets user basic detail.
        /// </summary>
        /// <param name="changePasswordToken">The changePasswordToken.</param>
        /// /// <param name="urlExpiryTime">The urlExpiryTime.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public string validateChangePasswordURL(string changePasswordToken, int urlExpiryTime)
        {
            string validationStatus = "Invalid";
            var userDetail = _unitOfWork.UserRepository.GetMany(x => x.PasswordRequestHash == changePasswordToken).FirstOrDefault();
            if (userDetail != null)
            {
                validationStatus = DateTime.Now < userDetail.PasswordRequestDate.Value.AddMinutes(urlExpiryTime) ? userDetail.Email : "Expired";
            }
            return validationStatus;
        }

        #endregion

        

        /// <summary>
        /// Logins the Specified Username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>BusinessEntities.UserContext.</returns>
        public UserContext login(string username, string password)
        {
            var user = _unitOfWork.UserRepository.GetMany(s => s.Email.Equals(username, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (user != null && BusinessEntities.Cryptography.Cryptography.VerifyHash(password, user.PasswordHash))
            {
                return new UserContext
                {
                    Username = user.Email,
                    UserID = user.ID,
                    //AppID = user.fk_AppID,
                    Roles = user.UserRoles.Select(x => x.Role.RoleName).ToArray()
                };
            }
            return null;
        }

        public List<UserLoginHistoryVm> GetAllUserLogHistoryList(int? userID, DateTime dateFrom, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public bool AuthenticateUser(string username, string password)
        {
            var user = _unitOfWork.UserRepository.GetMany(s => s.Email.Equals(username, StringComparison.OrdinalIgnoreCase) && s.Active).FirstOrDefault();
            if (user != null && BusinessEntities.Cryptography.Cryptography.VerifyHash(password, user.PasswordHash))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Gets the User's Basic Information.
        /// </summary>
        /// <param name="userName">The username.</param>
        /// <returns>BusinessEntities.UserViewModel.</returns>
        public UserViewModel getUserBasicInfo(string userName)
        {
            return _unitOfWork.UserRepository.GetMany(s => s.Email.Equals(userName, StringComparison.OrdinalIgnoreCase)).Select(x => new UserViewModel()
            {
                UserId = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                MiddleName = x.MiddleName,
                LastPasswordChange = x.LastPasswordChange,
                Active = x.Active
            }).FirstOrDefault();
        }
        public List<DropDown> GetAllUsersDropDown()
        {
            return _unitOfWork.UserRepository.GetMany(x => x.Active).Select(x => new DropDown()
            {
                Value = x.ID.ToString(),
                Text = x.FirstName + " " + x.LastName
            }).ToList();
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns>List&lt;UserListVm&gt;.</returns>
        public List<UserInfoVM> GetAllUsers()
        {
            var result = new List<UserInfoVM>();
            // Please improve this query for long list
            result = _unitOfWork.UserRepository.GetMany(x => x.ID != 1).Select(x => new UserInfoVM()
            {
                UserId = x.ID,
                Email = x.Email,
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                DOB = x.DOB,
                Fax = x.Fax,
                fk_GenderID = x.fk_GenderID ?? 0,
                PhoneNo = x.PhoneNo,
                MobileNo = x.MobileNo,
                Active = x.Active,
                

                UserRoles = x.UserRoles.Select(r => new RoleVM
                {
                    RoleID = r.fk_RoleID,
                    RoleName = r.Role.RoleName,
                    Selected = true
                }).ToList(),
            }).OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToList();
            return result;
        }

        /// <summary>
        /// Gets the User's Basic Information.
        /// </summary>
        /// <param name="userID">The User ID.</param>
        /// <returns>BusinessEntities.UserViewModel.</returns>
        public UserViewModel getUserBasicInfoByID(int userId)
        {
            return _unitOfWork.UserRepository.GetMany(s => s.ID == userId).Select(x => new UserViewModel()
            {
                UserId = x.ID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                MiddleName = x.MiddleName,
                LastPasswordChange = x.LastPasswordChange,
                Active = x.Active
            }).FirstOrDefault();
        }
        /// <summary>
        ///Change user's password on reset password request or on first login.
        /// </summary>
        /// <param name="changePasswordRequestVM">The changePasswordRequestVM which contain all required perameters.</param>
        /// <returns>System.String as process status.</returns>
        public string ResetPassword(ChangePasswordRequestVM changePasswordRequestVM)
        {
            string userFeedback = "No record found";
            var user = _unitOfWork.UserRepository.GetMany(s => s.Email.Equals(changePasswordRequestVM.UserName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (user != null)
            {
                if (DateTime.Now < user.PasswordRequestDate.Value.AddMinutes(changePasswordRequestVM.UrlExpiryTime))
                {
                    user.PasswordHash = BusinessEntities.Cryptography.Cryptography.GeneratePasswordHash(changePasswordRequestVM.NewPassword);
                    user.LastPasswordChange = DateTime.Now;
                    user.PasswordRequestHash = null;

                    _unitOfWork.UserRepository.Update(user);
                    _unitOfWork.Save();

                    userFeedback = "Password changed successfully.";
                }
                else
                {
                    userFeedback = "URL has been expired.";
                }

            }
            return userFeedback;
        }
        public string ChangePassword(ChangePasswordRequestVM changePasswordRequestVM)
        {
            string userFeedback = "No record found";
            var user = _unitOfWork.UserRepository.GetMany(s => s.Email.Equals(changePasswordRequestVM.UserName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (user != null)
            {
                //if (DateTime.Now < user.PasswordRequestDate.Value.AddMinutes(changePasswordRequestVM.UrlExpiryTime))
                //{
                if (user != null && BusinessEntities.Cryptography.Cryptography.VerifyHash(changePasswordRequestVM.OldPassword, user.PasswordHash))
                {
                    user.PasswordHash = BusinessEntities.Cryptography.Cryptography.GeneratePasswordHash(changePasswordRequestVM.NewPassword);
                    user.LastPasswordChange = DateTime.Now;
                    user.PasswordRequestHash = null;

                    _unitOfWork.UserRepository.Update(user);
                    _unitOfWork.Save();

                    userFeedback = "Password changed successfully.";
                }
                else
                {
                    userFeedback = "Old Password is not correct";
                }

                //}
                //else
                //{
                //    userFeedback = "URL has been expired.";
                //}

            }
            return userFeedback;
        }

        

        /// <summary>
        /// Gets User Identifier(Id).
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>System.Int32.</returns>
        public int getUserID(string username)
        {
            return _unitOfWork.UserRepository.GetMany(s => s.Email.Equals(username, StringComparison.OrdinalIgnoreCase)).Select(x => x.ID).FirstOrDefault();
        }

        /// <summary>
        /// Gets user basic detail.
        /// </summary>
        /// <param name="changePasswordToken">The changePasswordToken.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>

        /// <summary>
        /// Update Active In-Active User.
        /// </summary>
        /// <param name="userID">The user identifier.</param>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ActiveInActiveUser(int userID, bool isActive)
        {
            var user = _unitOfWork.UserRepository.GetFirstOrDefault(u => u.ID == userID);
            if (user != null)
            {
                user.Active = isActive;
                _unitOfWork.Save();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Active and inactive user.
        /// </summary>

        public bool IsEmailExist(string email)
        {
            var result = _unitOfWork.UserRepository.GetFirstOrDefault(x => x.Email.ToUpper() == email.ToUpper());
            return result != null ? true : false;
        }
        /// <summary>
        /// Active and inactive user.
        /// </summary>


        #region Users Module


        public int UpsertUser(UserInfoVM userinfo)
        {

            try
            {
                bool isNew = false;
                bool isProviderNew = false;
                var result = _unitOfWork.UserRepository.GetFirstOrDefault(x => x.ID == userinfo.UserId || x.Email.ToUpper() == userinfo.Email.ToUpper());
                if (result == null)
                {
                    result = new UserInfo();
                    result.CreatedDate = userinfo.CreatedDate;
                    isNew = true;
                }
                result.FirstName = userinfo.FirstName;
                result.MiddleName = userinfo.MiddleName;
                result.LastName = userinfo.LastName;
                result.Email = userinfo.Email;
                result.Fax = userinfo.Fax;
                result.PhoneNo = userinfo.PhoneNo;
                result.MobileNo = userinfo.MobileNo;
                result.fk_GenderID = userinfo.fk_GenderID == null || userinfo.fk_GenderID == 0 ? GenderEnum.Male.GetHashCode() : userinfo.fk_GenderID;
                result.ModifiedDate = userinfo.ModifiedDate;
                result.Active = userinfo.Active;

                

                if (isNew) // Add UserInfo, If new
                {
                    _unitOfWork.UserRepository.Insert(result);
                    _unitOfWork.Save();
                }

                if (!isNew && result.ID > 0)
                {
                    var userRoleData = _unitOfWork.UserRoleRepository.GetMany(x => x.fk_UserID == result.ID).ToList();
                    if (userRoleData != null && userRoleData.Count > 0)
                    {
                        _unitOfWork.UserRoleRepository.DeleteRange(userRoleData);
                    }
                }
                // Now Add all newly assigned 
                List<UserRole> roles = new List<UserRole>();
                foreach (var item in userinfo.UserRoles)
                {
                    UserRole role = new UserRole();
                    role.fk_RoleID = item.RoleID;
                    role.fk_UserID = result.ID;
                    // roles.Add(role);
                    _unitOfWork.UserRoleRepository.Insert(role);
                }



                _unitOfWork.Save();
                return result.ID;
            }

            catch (Exception ex)
            {
                throw new Exception("Error on saving user." + ex.Message);
            }
        }

        public List<RoleVM> GetAllRoles()
        {
            List<RoleVM> result = new List<RoleVM>();
            result = _unitOfWork.RoleRepository.GetAll().Select(x => new RoleVM()
            {
                RoleID = x.RoleId,
                RoleName = x.RoleName,
                IsActive = x.IsActive,
            }).OrderBy(o => o.RoleName).ToList();
            return result;
        }
        public List<RoleVM> GetUserRoles(int userID)
        {
            List<RoleVM> result = new List<RoleVM>();
            result = _unitOfWork.RoleRepository.GetMany(x => x.IsActive).Select(x => new RoleVM()
            {
                RoleID = x.RoleId,
                RoleName = x.RoleName,
                IsActive = x.IsActive,
                Selected = x.UserRoles.Any(y => y.fk_UserID == userID)
            }).OrderBy(o => o.RoleName).ToList();
            return result;
        }
        public bool UpSertRole(RoleVM role)
        {
            var roleObj = _unitOfWork.RoleRepository.GetSingle(x => x.RoleId == role.RoleID);
            // Add new Role
            if (roleObj == null)
                roleObj = new Role();

            // Update Role
            roleObj.RoleName = role.RoleName;
            roleObj.IsActive = role.IsActive;

            if (role.RoleID == 0) // in case of Add
                _unitOfWork.RoleRepository.Insert(roleObj);

            _unitOfWork.Save();
            return true;
        }
        public bool AttachDetacheUserRoles(int userID, List<int> roleIDs)
        {
            bool result = false;
            try
            {
                var userRoleData = _unitOfWork.UserRoleRepository.GetMany(x => x.ID == userID).ToList();
                if (userRoleData != null && userRoleData.Count > 0)
                {
                    _unitOfWork.UserRoleRepository.DeleteRange(userRoleData);
                    _unitOfWork.Save();
                }
                // Now Add all newly assigned 
                List<UserRole> roles = new List<UserRole>();
                foreach (var item in roleIDs)
                {
                    var role = new UserRole();
                    role.fk_RoleID = item;
                    role.fk_UserID = userID;
                    _unitOfWork.UserRoleRepository.Insert(role);
                }
                _unitOfWork.Save();
                result = true;
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        public bool AttachDetacheRoleUsers(int roleID, List<int> userIDs)
        {
            bool result = false;
            try
            {
                var userRoleData = _unitOfWork.UserRoleRepository.GetMany(x => x.fk_RoleID == roleID).ToList();
                if (userRoleData != null && userRoleData.Count > 0)
                {
                    _unitOfWork.UserRoleRepository.DeleteRange(userRoleData);
                    _unitOfWork.Save();
                }
                // Now Add all newly assigned 
                var roles = new List<UserRole>();
                foreach (var user in userIDs)
                {
                    var role = new UserRole();
                    role.fk_RoleID = roleID;
                    role.fk_UserID = user;
                    //roles.Add(role);
                    _unitOfWork.UserRoleRepository.Insert(role);
                }
                //db.UserToRoles.AddRange(roles);
                _unitOfWork.Save();
                result = true;
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        /// <summary>
        /// Generates the password.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <param name="numberOfNonAlphanumericCharacters">The number of non alphanumeric characters.</param>
        /// <returns>System.String.</returns>
        public string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
        {
            return System.Web.Security.Membership.GeneratePassword(length, numberOfNonAlphanumericCharacters);
        }
        public List<UserInfoVM> GetAssignedRoleUsers(int roleID)
        {
            List<UserInfoVM> result = new List<UserInfoVM>();
            try
            {
                result = _unitOfWork.UserRepository.GetMany(x => x.Active)
                       .Select(x => new UserInfoVM()
                       {
                           UserId = x.ID,
                           //FullName = x.FirstName + " " + x.LastName,
                           //RoleName = x.RoleName,
                           //IsActive = x.IsActive,
                           Selected = x.UserRoles.Any(y => y.fk_RoleID == roleID)
                       }).OrderBy(o => o.FullName).ToList();
            }
            catch (Exception ex)
            {
            }
            return result;
        }

        #endregion


    }
}