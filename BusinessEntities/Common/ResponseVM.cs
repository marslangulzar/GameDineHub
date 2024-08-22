namespace BusinessEntities.Entities.Common
{
    public class ResponseVM
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string data { get; set; }
        public object Custom { get; set; }
        public bool IsSuccess { get; set; }
    }
}
