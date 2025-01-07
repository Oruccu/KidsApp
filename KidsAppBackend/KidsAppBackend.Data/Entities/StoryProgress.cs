public class StoryProgress : BaseEntity
{
    public int ChildId { get; set; }
    public ChildUser Child { get; set; }
    public int StoryId { get; set; }
    public int CompletionPercentage { get; set; }
}
