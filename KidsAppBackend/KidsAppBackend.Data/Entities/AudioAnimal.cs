using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KidsAppBackend.Data.Entities;

namespace KidsAppBackend.Data.Entities
{
    public class AudioAnimal : BaseEntity
    {
        public string AnimalName { get; set; }
        public string AudioFileUrl { get; set; }
    }
}

// AudioAnimal Configuration
public class AudioAnimalConfiguration : BaseConfiguration<AudioAnimal>
{
    public override void Configure(EntityTypeBuilder<AudioAnimal> builder)
    {
        base.Configure(builder);
        builder.Property(a => a.AnimalName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(a => a.AudioFileUrl)
               .IsRequired()
               .HasMaxLength(200);
    }
}
