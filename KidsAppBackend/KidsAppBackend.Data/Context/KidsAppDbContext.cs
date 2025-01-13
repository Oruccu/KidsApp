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
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        // Veritabanı tablolarını temsil eden DbSet özellikleri
        public DbSet<ChildUser> ChildUsers { get; set; }
        public DbSet<KidsMode> KidsModes { get; set; }
        public DbSet<GameResult> GameResults { get; set; }
        public DbSet<StoryProgress> StoryProgresses { get; set; }
        public DbSet<AudioBook> AudioBooks { get; set; }
        public DbSet<AudioAnimal> AudioAnimals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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

            modelBuilder.Entity<ChildUser>().HasData(
                new ChildUser
                {
                    Id = 1,
                    Email = "child1@example.com",
                    Username = "Child1",
                    Password = "Test123",
                    ParentUserName = "Parent1",
                    CreatedAt = DateTime.Now
                });

            modelBuilder.Entity<GameResult>().HasData(
                new GameResult
                {
                    Id = 1,
                    ChildId = 1,
                    GameType = Data.Enums.GameType.LearnAnimals,
                    Score = 85,
                    DatePlayed = DateTime.Now,
                    CreatedAt = DateTime.Now
                });
        }

    }


}
