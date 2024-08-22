using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Entities.Account
{
    public class AccountFirstLoginVM
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>The email.</value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the UserID.
        /// </summary>
        /// <value>The email.</value>
        public int UserID { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        /// 
        public string Password { get; set; }
    }
}
