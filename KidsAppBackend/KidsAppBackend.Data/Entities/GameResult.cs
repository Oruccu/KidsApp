public class GameResult : BaseEntity
{
    public int ChildId { get; set; }
    public ChildUser Child { get; set; }
    public GameType GameType { get; set; }
    public int Score { get; set; }
    public DateTime DatePlayed { get; set; }
}
