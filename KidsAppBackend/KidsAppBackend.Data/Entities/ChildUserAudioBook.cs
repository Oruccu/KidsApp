using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsAppBackend.Data.Entities
{
    public class ChildUserAudioBook: BaseEntity
    {
        public int ChildUserId { get; set; }
        public ChildUser ChildUser { get; set; }

        public int AudioBookId { get; set; }
        public AudioBook AudioBook { get; set; }
    }
    public class ChildUserAudioBookConfiguration : IEntityTypeConfiguration<ChildUserAudioBook>
    {
        public void Configure(EntityTypeBuilder<ChildUserAudioBook> builder)
        {

            builder.HasKey(cuab => new { cuab.ChildUserId, cuab.AudioBookId });

            builder.HasOne(cuab => cuab.ChildUser)
                   .WithMany()  
                   .HasForeignKey(cuab => cuab.ChildUserId);

            builder.HasOne(cuab => cuab.AudioBook)
                   .WithMany()  
                   .HasForeignKey(cuab => cuab.AudioBookId);
        }
    }
}
