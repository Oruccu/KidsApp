namespace KidsAppBackend.Business.Types
{
    public class ServiceMessage
    {
        public bool IsSucced { get; set; }
        public string Message { get; set; } = string.Empty;
        public int? ParentId { get; set; }
    }
}
