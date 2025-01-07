using KidsAppBackend.Data.Entities;
using KidsAppBackend.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace KidsAppBackend.Data
{
    public class KidsAppDbContext : DbContext
    {
        public KidsAppDbContext(DbContextOptions<KidsAppDbContext> options)
            : base(options)
        {
        }

        // DbSet Properties
        public DbSet<ParentUser> ParentUsers { get; set; }
        public DbSet<ChildUser> ChildUsers { get; set; }
        public DbSet<KidsMode> KidsModes { get; set; }
        public DbSet<GameResult> GameResults { get; set; }
        public DbSet<StoryProgress> StoryProgresses { get; set; }
        public DbSet<AudioBook> AudioBooks { get; set; }
        public DbSet<AudioAnimal> AudioAnimals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Entity Configurations
            modelBuilder.Entity<ParentUser>(entity =>
            {
                entity.HasMany(p => p.Children)
                      .WithOne(c => c.Parent)
                      .HasForeignKey(c => c.ParentUserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ChildUser>(entity =>
            {
                entity.HasMany(c => c.GameResults)
                      .WithOne(g => g.Child)
                      .HasForeignKey(g => g.ChildId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(c => c.StoryProgresses)
                      .WithOne(s => s.Child)
                      .HasForeignKey(s => s.ChildId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GameResult>(entity =>
            {
                entity.Property(g => g.GameType)
                      .HasConversion<string>(); 
            });

            // Seed Data (Opsiyonel)
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<GameResult>().HasData(
                new GameResult
                {
                    Id = 1,
                    ChildId = 1,
                    GameType = GameType.LearnAnimals,
                    Score = 85,
                    DatePlayed = DateTime.UtcNow
                }
            );

            modelBuilder.Entity<ChildUser>().HasData(
                new ChildUser
                {
                    Id = 1,
                    Email = "child1@example.com",
                    Username = "Child1",
                    Password = "EncryptedPassword123",
                    ParentUserId = 1
                }
            );

            modelBuilder.Entity<ParentUser>().HasData(
                new ParentUser
                {
                    Id = 1,
                    Email = "parent1@example.com",
                    Password = "EncryptedPassword456"
                }
            );
        }
    }
}
