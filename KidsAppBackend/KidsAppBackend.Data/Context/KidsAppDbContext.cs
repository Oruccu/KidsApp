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

        public DbSet<ChildUser> ChildUsers { get; set; }
        public DbSet<KidsMode> KidsModes { get; set; }
        public DbSet<GameResult> GameResults { get; set; }
        public DbSet<StoryProgress> StoryProgresses { get; set; }
        public DbSet<AudioBook> AudioBooks { get; set; }
        public DbSet<AudioAnimal> AudioAnimals { get; set; }
        public DbSet<ChildUserAudioBook> ChildUserAudioBooks { get; set; } // Join tablo

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            modelBuilder.ApplyConfiguration(new ChildUserConfiguration());
            modelBuilder.ApplyConfiguration(new KidsModeConfiguration());
            modelBuilder.ApplyConfiguration(new GameResultConfiguration());
            modelBuilder.ApplyConfiguration(new StoryProgressConfiguration());
            modelBuilder.ApplyConfiguration(new AudioBookConfiguration());
            modelBuilder.ApplyConfiguration(new AudioAnimalConfiguration());
            modelBuilder.ApplyConfiguration(new ChildUserAudioBookConfiguration());


            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // -------------------- 1) ChildUser -----------------------
            modelBuilder.Entity<ChildUser>().HasData(
                new ChildUser
                {
                    Id = 1,
                    Email = "child1@example.com",
                    Username = "Child1",
                    Password = "Test123",      
                    ParentUserName = "Parent1",
                    Score = 0,
                    CreatedAt = DateTime.Now
                },
                new ChildUser
                {
                    Id = 2,
                    Email = "child2@example.com",
                    Username = "Child2",
                    Password = "Test123",
                    ParentUserName = "Parent2",
                    Score = 50,
                    CreatedAt = DateTime.Now
                }
            );

            // -------------------- 2) KidsMode -----------------------
            modelBuilder.Entity<KidsMode>().HasData(
                new KidsMode
                {
                    Id = 1,
                    ChildId = 1,
                    Mode = ModeType.Girl,
                    CreatedAt = DateTime.Now
                },
                new KidsMode
                {
                    Id = 2,
                    ChildId = 2,
                    Mode = ModeType.Boy,
                    CreatedAt = DateTime.Now
                }
            );

            // -------------------- 3) GameResult -----------------------
            modelBuilder.Entity<GameResult>().HasData(
                new GameResult
                {
                    Id = 1,
                    ChildId = 1,
                    GameType = Data.Enums.GameType.LearnAnimals,
                    Score = 85,
                    DatePlayed = DateTime.Now,
                    CreatedAt = DateTime.Now
                },
                new GameResult
                {
                    Id = 2,
                    ChildId = 1,
                    GameType = Data.Enums.GameType.LearnColor,
                    Score = 90,
                    DatePlayed = DateTime.Now,
                    CreatedAt = DateTime.Now
                },
                new GameResult
                {
                    Id = 3,
                    ChildId = 2,
                    GameType = Data.Enums.GameType.LearnAnimals,
                    Score = 70,
                    DatePlayed = DateTime.Now,
                    CreatedAt = DateTime.Now
                }
            );

            // -------------------- 4) StoryProgress -----------------------
            modelBuilder.Entity<StoryProgress>().HasData(
                new StoryProgress
                {
                    Id = 1,
                    ChildId = 1,
                    StoryId = 101,
                    CompletionPercentage = 50,
                    CreatedAt = DateTime.Now
                },
                new StoryProgress
                {
                    Id = 2,
                    ChildId = 2,
                    StoryId = 102,
                    CompletionPercentage = 100,
                    CreatedAt = DateTime.Now
                }
            );

            // -------------------- 5) AudioBook -----------------------
            modelBuilder.Entity<AudioBook>().HasData(
                new AudioBook
                {
                    Id = 1,
                    Title = "The Lion and The Mouse",
                    AudioFileUrl = "http://example.com/audiofiles/lionmouse.mp3",
                    CreatedAt = DateTime.Now
                },
                new AudioBook
                {
                    Id = 2,
                    Title = "Little Red Riding Hood",
                    AudioFileUrl = "http://example.com/audiofiles/redridinghood.mp3",
                    CreatedAt = DateTime.Now
                }
            );

            // -------------------- 6) AudioAnimal -----------------------
            modelBuilder.Entity<AudioAnimal>().HasData(
                new AudioAnimal
                {
                    Id = 1,
                    AnimalName = "Cat",
                    AudioFileUrl = "http://example.com/audiofiles/cat_meow.mp3",
                    CreatedAt = DateTime.Now
                },
                new AudioAnimal
                {
                    Id = 2,
                    AnimalName = "Dog",
                    AudioFileUrl = "http://example.com/audiofiles/dog_bark.mp3",
                    CreatedAt = DateTime.Now
                }
            );

            // -------------------- 7) ChildUserAudioBook (Many-to-many Join) -----------------------
          

            modelBuilder.Entity<ChildUserAudioBook>().HasData(

                new ChildUserAudioBook
                {
                    ChildUserId = 1,
                    AudioBookId = 1,
                    CreatedAt = DateTime.Now
                },
                new ChildUserAudioBook
                {
                    ChildUserId = 1,
                    AudioBookId = 2,
                    CreatedAt = DateTime.Now
                },
                new ChildUserAudioBook
                {
                    ChildUserId = 2,
                    AudioBookId = 1,
                    CreatedAt = DateTime.Now
                }
            );
        }
    }
}
