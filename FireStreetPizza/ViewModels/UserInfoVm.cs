using BusinessEntities.Entities;
using System;
using System.Collections.Generic;

namespace FireStreetPizza.ViewModels
{
    public class UserViewModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>The user identifier.</value>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the Middle name
        /// </summary>
        /// <value> The middle name.</value>
        public string MiddleName { get; set; }
        /// <summary>
        /// Gets or sets the Last name
        /// </summary>
        /// <value> The last name.</value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets the fullname
        /// </summary>
        /// <value> The full name(FirstName + MiddleName + LastName).</value>
        public string FullName { get { return FirstName + " " + MiddleName + " " + LastName; } }
        /// <summary>
        /// Gets or Sets the LastPasswordChange.
        /// </summary>
        /// <value>The LastPasswordChange.</value>
        public DateTime? LastLogin { get; set; }
        public bool Active { get; set; }
    }

    public class UserInfoVM
    {
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the Hub User ID.
        /// </summary>
        /// <value>The Hub user identifier.</value>
        public int HubUID { get; set; }
        /// <summary>
        /// Gets or sets the UserLogin DateTime.
        /// </summary>
        /// <value>The User DateTime.</value>
        public DateTime? LoginDateTime { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName { get; set; }
        /// <summary>
        /// Gets or sets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="UserInfoVM"/> is active.
        /// </summary>
        /// <value><c>true</c> if active; otherwise, <c>false</c>.</value>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the last login.
        /// </summary>
        /// <value>The last login.</value>
        public Nullable<DateTime> LastLogin { get; set; }
        /// <summary>
        /// Gets or sets the user roles.
        /// </summary>
        /// <value>The user roles.</value>
        public List<RoleVM> UserRoles { get; set; }
    }
}