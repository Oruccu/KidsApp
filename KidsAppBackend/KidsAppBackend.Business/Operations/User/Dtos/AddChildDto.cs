namespace KidsAppBackend.Business.Operations.User.Dtos
{
    public class AddChildDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int ParentUserId { get; set; }
    }
}
