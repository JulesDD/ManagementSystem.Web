using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementSystem.Web.Data.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.HasData(
            new ApplicationUser
            {
                Id = "d1b9c8e5-9c3a-4f0e-8b1a-2c3e4f5a6b7c",
                Email = "admin@localhost",
                NormalizedEmail = "ADMIN@LOCALHOST",
                UserName = "admin@localhost",
                NormalizedUserName = "ADMIN@LOCALHOST",
                PasswordHash = "AQAAAAIAAYagAAAAEBqQW8v6VwJz0l5M4Y7n5tYxY0ZzYv3FZp1v8s7zVqzYvP0Zl6F3mX9VxY5m9w==",
                SecurityStamp = "b9c1f2d3-4e5f-6789-abcd-1234567890ab",
                ConcurrencyStamp = "c1a2b3d4-e5f6-7890-abcd-1234567890ef",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "User",
                DateOfBirth = new DateOnly(1990, 1, 1)
            });
    }
}
