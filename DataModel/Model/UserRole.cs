//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataModel.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserRole
    {
        public int ID { get; set; }
        public int fk_UserID { get; set; }
        public int fk_RoleID { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual UserInfo UserInfo { get; set; }
    }
}
