using System;
using KidsAppBackend.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KidsAppBackend.Data.Entities;

namespace KidsAppBackend.Data.Entities
{
    public class KidsMode : BaseEntity
    {
        public bool Boy { get; set; }
        public bool Girl { get; set; }
        public int ChildId { get; set; }
        public ChildUser Child { get; set; }
    }

}

// KidsMode Configuration
public class KidsModeConfiguration : BaseConfiguration<KidsMode>
{
    public override void Configure(EntityTypeBuilder<KidsMode> builder)
    {
        base.Configure(builder);
        builder.Property(k => k.Boy).IsRequired();
        builder.Property(k => k.Girl).IsRequired();

        builder.HasOne(k => k.Child)
               .WithMany()
               .HasForeignKey(k => k.ChildId);
    }
}

