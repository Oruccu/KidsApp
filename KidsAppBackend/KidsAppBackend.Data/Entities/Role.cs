using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsAppBackend.Data.Entities
{
    public class Role : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
    }

    public class RoleConfiguration : BaseConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);
            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasData(
                new Role { Id = 1, Name = "Parent" },
                new Role { Id = 2, Name = "Child" }
            );
        }
    }
}
