
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KidsAppBackend.Data.Entities;
namespace KidsAppBackend.Data.Entities
{
    public class ChildUser : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int ParentUserId { get; set; }
        
        // Bu satırı "new ParentUser()" ile başlatmayın:
        public ParentUser Parent { get; set; } 

        public ICollection<GameResult> GameResults { get; set; } = new List<GameResult>();
        public ICollection<StoryProgress> StoryProgresses { get; set; } = new List<StoryProgress>();
    }

    // ChildUser Configuration
    public class ChildUserConfiguration : BaseConfiguration<ChildUser>
    {
        public override void Configure(EntityTypeBuilder<ChildUser> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Username)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.Password)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(c => c.Parent)
                   .WithMany(p => p.Children)
                   .HasForeignKey(c => c.ParentUserId);
        }
    }
}
