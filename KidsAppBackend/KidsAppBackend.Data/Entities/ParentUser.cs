using System;
using KidsAppBackend.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using KidsAppBackend.Data.Entities;

namespace KidsAppBackend.Data.Entities
{
    public class ParentUser : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<ChildUser> Children { get; set; }
    }
}

// ParentUser Configuration
public class ParentUserConfiguration : BaseConfiguration<ParentUser>
{
    public override void Configure(EntityTypeBuilder<ParentUser> builder)
    {
        base.Configure(builder);
        builder.Property(p => p.Email)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Password)
               .IsRequired()
               .HasMaxLength(100);
    }
}
