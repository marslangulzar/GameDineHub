namespace BusinessEntities.Entities.Common
{
    public class BaseModel
    {
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedByName { get; set; }
    }
}
