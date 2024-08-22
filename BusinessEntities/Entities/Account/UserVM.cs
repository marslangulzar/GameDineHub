using System;
using System.Collections.Generic;

namespace BusinessEntities.Entities
{
    public class UserVM
    {
        public int ID { get; set; }
        public Nullable<int> fk_GenderID { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string PasswordRequestHash { get; set; }
        public Nullable<System.DateTime> PasswordRequestDate { get; set; }
        public int LoginAttempts { get; set; }
        public Nullable<System.DateTime> LastPasswordChange { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string PhoneExt { get; set; }
        public string Fax { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public System.DateTime? DOB { get; set; }
        public bool? IsNewsLetter { get; set; }
        public string DOBString
        {
            get; set;
            //{
            //    return DOB != null && DOB.Value != DateTime.MinValue ? DOB.Value.ToShortDateString() : "N/A";
            //}
        }
        public bool Active { get; set; }
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
        /// <summary>
        /// Gets or sets the last login.
        /// </summary>
        /// <value>The last login.</value>
        public DateTime? LastLogin { get; set; }

    }

}

