using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Entities.Account
{
    public class UserAccountVM : UserVM
    {
        public bool IsUserAuthenticated { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
