using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace KidsAppBackend.Data.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; } 
        public DateTime CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; } 
        public bool IsDeleted { get; set; } = false; 
        public DateTime? ModifiedDate { get; set; } 
    }

    // Base Configuration Class
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            // Primary key tanımı
            builder.HasKey(e => e.Id);

            // Zorunlu alanlar
            builder.Property(e => e.CreatedAt)
                   .IsRequired();

            builder.Property(e => e.UpdatedAt);

            // Soft delete için alan tanımı
            builder.Property(e => e.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(e => e.ModifiedDate);
        }
    }
}
