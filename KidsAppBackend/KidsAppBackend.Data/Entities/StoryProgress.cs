using System;
using KidsAppBackend.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KidsAppBackend.Data.Entities;

namespace KidsAppBackend.Data.Entities
{
    public class StoryProgress : BaseEntity
    {
        public int ChildId { get; set; }
        public ChildUser Child { get; set; }
        public int StoryId { get; set; }
        public int CompletionPercentage { get; set; }
    }
}


// StoryProgress Configuration
public class StoryProgressConfiguration : BaseConfiguration<StoryProgress>
{
    public override void Configure(EntityTypeBuilder<StoryProgress> builder)
    {
        base.Configure(builder);
        builder.Property(s => s.CompletionPercentage)
               .IsRequired();

        builder.HasOne(s => s.Child)
               .WithMany(c => c.StoryProgresses)
               .HasForeignKey(s => s.ChildId);
    }
}

