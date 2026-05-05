using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Web.Data;


public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // This method is used to configure the model that was discovered by convention from the entity types
    override protected void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "15605dde-16ce-4c93-832c-ded1c5231fd0", Name = "Employee", NormalizedName = "EMPLOYEE", ConcurrencyStamp = "role-emp-001" },
            new IdentityRole { Id = "5414e681-f604-48c6-b46b-dc170cec5a6b", Name = "Supervisor", NormalizedName = "SUPERVISOR", ConcurrencyStamp = "role-sup-001" },
            new IdentityRole { Id = "52a5d602-2cd4-4a20-aaf5-6c4a8bfbb718", Name = "Administrator", NormalizedName = "ADMINISTRATOR", ConcurrencyStamp = "role-admin-001" }
        );
        var hasher = new PasswordHasher<ApplicationUser>();
        builder.Entity<ApplicationUser>().HasData(
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
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "52a5d602-2cd4-4a20-aaf5-6c4a8bfbb718",
                UserId = "d1b9c8e5-9c3a-4f0e-8b1a-2c3e4f5a6b7c"
            });
    }
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveQuota> LeaveQuotas { get; set; }
    public DbSet<Period> Periods { get; set; }

    }
