using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities.Common
{
    public class UserLoginHistoryVm
    {
        public int UserLoginHistoryID { get; set; }
        public int fk_UserId { get; set; }
        public System.DateTime LoginDateTime { get; set; }
        public Nullable<System.DateTime> LogoutDateTime { get; set; }
        public string UserIP { get; set; }
        public string BrowserName { get; set; }
        public string UserName { get; set; }
        public string LogType { get; set; }
        public object ID { get; set; }
    }
    public class ManageAllUserLogHistoryVm
    {
        public List<DropDown> UserList { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<UserLoginHistoryVm> AllUsersLogHistory { get; set; }
    }
}
