using KidsAppBackend.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace KidsAppBackend.Data.Entities
{
    public enum ModeType
    {
        Boy,
        Girl
    }
    public class KidsMode : BaseEntity
    {
        public ModeType Mode { get; set; } 
        public int ChildId { get; set; }
        public ChildUser Child { get; set; }
    }
    public class KidsModeConfiguration : BaseConfiguration<KidsMode>
    {
        public override void Configure(EntityTypeBuilder<KidsMode> builder)
        {
            base.Configure(builder);
            builder.Property(k => k.Mode)
                   .IsRequired()
                   .HasConversion<string>() // Enum'u string olarak depolamak için
                   .HasMaxLength(10);

            builder.HasOne(k => k.Child)
                   .WithMany()
                   .HasForeignKey(k => k.ChildId);
        }
    }
}
