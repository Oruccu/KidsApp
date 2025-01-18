namespace KidsAppBackend.Business.Types
{
    public class ResultDto
    {
        public bool IsSucced { get; set; }
        public string Message { get; set; } 
        public string Token { get; set; }
        public object Data { get; set; } 
        public string UserName { get; set;}
    }
}
