namespace BusinessEntities.Entities
{
    public class RoleVM
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
    }
}
