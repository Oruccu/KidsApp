using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KidsAppBackend.Data.Entities
{
    public class UserRole
    {
        public int ChildUserId { get; set; }
        public ChildUser ChildUser { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }

    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => new { ur.ChildUserId, ur.RoleId });

            builder.HasOne(ur => ur.ChildUser)
                   .WithMany(c => c.UserRoles)
                   .HasForeignKey(ur => ur.ChildUserId);

            builder.HasOne(ur => ur.Role)
                   .WithMany()
                   .HasForeignKey(ur => ur.RoleId);

            builder.HasData(
                new UserRole { ChildUserId = 1, RoleId = 2 }, // Child
                new UserRole { ChildUserId = 2, RoleId = 2 }
            );
        }
    }
}
