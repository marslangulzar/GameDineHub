using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Common
{
    /// <summary>
    /// VM for Change Password Request.
    /// </summary>
    public class ChangePasswordRequestVM
    {
        /// <summary>
        /// Gets or sets the UserName.
        /// </summary>
        /// <value>The user email.</value>
        public string UserName { get; set; }
        /// <summary>
        /// Gets or sets the OldPassword.
        /// </summary>
        /// <value>User's current password.</value>
        public string OldPassword { get; set; }
        /// <summary>
        /// Gets or sets the NewPassword.
        /// </summary>
        /// <value>Newly specified password by user.</value>
        public string NewPassword { get; set; }
        /// <summary>
        /// Gets or sets the IsForgotPasswordRequest.
        /// </summary>
        /// <value>bool value for marking a request as ForgotPassword Request which will use to check URL expiry.</value>
        public bool IsForgotPasswordRequest { get; set; }
        /// <summary>
        /// Gets or sets the UrlExpiryTime.
        /// </summary>
        /// <value>URL expiry time from config file.</value>
        public int UrlExpiryTime { get; set; }
    }
}
