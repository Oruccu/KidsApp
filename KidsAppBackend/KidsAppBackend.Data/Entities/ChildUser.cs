
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KidsAppBackend.Data.Entities;
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

