using BusinessEntities.Entities.Common;
using System;
using System.Collections.Generic;

namespace BusinessEntities.Entities.Account
{
    public class UserContext
    {
        public string Username { get; set; }
        public int UserID { get; set; }
        public string[] Roles { get; set; }
    }
    public class UserViewModel : BaseModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public int? fk_GenderID { get; set; }
        public int fk_GenderName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public string EmailAddress { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string PhoneExt { get; set; }
        public string Fax { get; set; }
        public DateTime? LastPasswordChange { get; set; }
        public DateTime? DOB { get; set; }
        public bool Active { get; set; }
        public int UserLoginHistoryID { get; set; }
        public int? StateID { get; set; }
    }
    public class UserInfoVM : UserViewModel
    {
        public int? Workfk_AddressTypeID { get; set; } // Add
        public int Workfk_StateID { get; set; } // Add
        public string WorkAddLine1 { get; set; } // Add
        public string WorkAddLine2 { get; set; } // Add
        public string WorkCity { get; set; } // Add
        public string WorkZipCode { get; set; } // Add
        public bool? WorkVerified { get; set; } // Add
        public bool? WorkActive { get; set; } // Add

        public bool IsAdmin { get; set; }
        public string External_Auth_Token { get; set; }
        public DateTime LoginDateTime { get; set; }
        public DateTime? LastLogin { get; set; }
        public List<RoleVM> UserRoles { get; set; }
        public bool Selected { get; set; }
        public string Password { get; set; }
        public bool FlagSetPassword { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }

        // Custom
        public int? TeamId { get; set; }
    }
}