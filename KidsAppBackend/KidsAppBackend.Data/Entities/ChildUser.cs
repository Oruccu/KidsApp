namespace KidsAppBackend.Data.Entities
{
    public class ChildUser : BaseEntity
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int ParentUserId { get; set; }
        public ParentUser Parent { get; set; }
        public ICollection<GameResult> GameResults { get; set; }
        public ICollection<StoryProgress> StoryProgresses { get; set; }
    }
}
