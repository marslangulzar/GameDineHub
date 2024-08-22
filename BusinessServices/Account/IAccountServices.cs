using BusinessEntities.Common;
using BusinessEntities.Entities;
using BusinessEntities.Entities.Account;
using System;
using System.Collections.Generic;

namespace BusinessServices.Account
{
    public interface IAccountServices
    {
        List<RoleVM> GetUserCurrentRoles(int userID);
        UserVM GetUserByEmail(string email);
        List<DropDown> GetAllUsersDropDown();
        List<UserLoginHistoryVm> GetAllUserLogHistoryList(int? userID, DateTime dateFrom, DateTime dateTime);
        bool AuthenticateUser(string username, string password);
        List<RoleVM> GetAllRoles();
        UserViewModel getUserBasicInfo(string userName);
        List<UserInfoVM> GetAllUsers();
        int UpsertUser(UserInfoVM userInfoVm);
        bool ActiveInActiveUser(int userID, bool active);
        string ResetPassword(ChangePasswordRequestVM changePasswordRequestVM);
        string ChangePassword(ChangePasswordRequestVM changePasswordRequestVM);
        bool updateChangePasswordRequestHash(string userName, string changePasswordToken);
        string validateChangePasswordURL(string changePasswordToken, int urlExpiryTime);
        //List<AccessModulesVM> GetAccessesOfAllRoles();
        //List<AccessModuleVM> GetAllAccessesOfAllModules();
        bool AttachDetacheRoleUsers(int roleID, List<int> userIDs);
        bool AttachDetacheUserRoles(int userID, List<int> roleIDs);
        List<UserInfoVM> GetAssignedRoleUsers(int roleID);
        List<RoleVM> GetUserRoles(int userID);
        bool UpSertRole(RoleVM role);
        bool IsEmailExist(string email);


    }
}
