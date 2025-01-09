using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KidsAppBackend.Data.Entities;

namespace KidsAppBackend.Data
{
    public class KidsAppDbContext : DbContext
    {
        public KidsAppDbContext(DbContextOptions<KidsAppDbContext> options)
            : base(options)
        {
        }

        // Veritabanı tablolarını temsil eden DbSet özellikleri
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

            // Varlıkların yapılandırmaları
            modelBuilder.ApplyConfiguration(new ParentUserConfiguration());
            modelBuilder.ApplyConfiguration(new ChildUserConfiguration());
            modelBuilder.ApplyConfiguration(new KidsModeConfiguration());
            modelBuilder.ApplyConfiguration(new GameResultConfiguration());
            modelBuilder.ApplyConfiguration(new StoryProgressConfiguration());
            modelBuilder.ApplyConfiguration(new AudioBookConfiguration());
            modelBuilder.ApplyConfiguration(new AudioAnimalConfiguration());

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ParentUser>().HasData(
                new ParentUser
                {
                    Id = 1,
                    Email = "parent1@example.com",
                    Password = "EncryptedPassword456",
                    CreatedAt = DateTime.UtcNow
                });

            modelBuilder.Entity<ChildUser>().HasData(
                new ChildUser
                {
                    Id = 1,
                    Email = "child1@example.com",
                    Username = "Child1",
                    Password = "EncryptedPassword123",
                    ParentUserId = 1,
                    CreatedAt = DateTime.UtcNow
                });

            modelBuilder.Entity<GameResult>().HasData(
                new GameResult
                {
                    Id = 1,
                    ChildId = 1,
                    GameType = Data.Enums.GameType.LearnAnimals,
                    Score = 85,
                    DatePlayed = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow
                });
        }

    }


}
