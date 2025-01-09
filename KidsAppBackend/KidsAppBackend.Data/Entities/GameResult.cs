using System;
using KidsAppBackend.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KidsAppBackend.Data.Entities;

namespace KidsAppBackend.Data.Entities
{
    public class GameResult : BaseEntity
    {
        public int ChildId { get; set; }
        public ChildUser Child { get; set; }
        public GameType GameType { get; set; }
        public int Score { get; set; }
        public DateTime DatePlayed { get; set; }
    }

}

// GameResult Configuration
public class GameResultConfiguration : BaseConfiguration<GameResult>
{
    public override void Configure(EntityTypeBuilder<GameResult> builder)
    {
        base.Configure(builder);
        builder.Property(g => g.Score)
               .IsRequired();

        builder.Property(g => g.DatePlayed)
               .IsRequired();

        builder.HasOne(g => g.Child)
               .WithMany(c => c.GameResults)
               .HasForeignKey(g => g.ChildId);
    }
}

