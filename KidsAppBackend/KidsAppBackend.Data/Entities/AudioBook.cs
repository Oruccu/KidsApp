using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KidsAppBackend.Data.Entities;

namespace KidsAppBackend.Data.Entities
{
    public class AudioBook : BaseEntity
    {
        public string Title { get; set; }
        public string AudioFileUrl { get; set; }
    }
}

// AudioBook Configuration
public class AudioBookConfiguration : BaseConfiguration<AudioBook>
{
    public override void Configure(EntityTypeBuilder<AudioBook> builder)
    {
        base.Configure(builder);
        builder.Property(b => b.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(b => b.AudioFileUrl)
               .IsRequired()
               .HasMaxLength(200);
    }
}

