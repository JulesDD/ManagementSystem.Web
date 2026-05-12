using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManagementSystem.Web.Data.Configurations;

public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole { Id = "15605dde-16ce-4c93-832c-ded1c5231fd0", Name = "Employee", NormalizedName = "EMPLOYEE", ConcurrencyStamp = "role-emp-001" },
            new IdentityRole { Id = "5414e681-f604-48c6-b46b-dc170cec5a6b", Name = "Supervisor", NormalizedName = "SUPERVISOR", ConcurrencyStamp = "role-sup-001" },
            new IdentityRole { Id = "52a5d602-2cd4-4a20-aaf5-6c4a8bfbb718", Name = "Administrator", NormalizedName = "ADMINISTRATOR", ConcurrencyStamp = "role-admin-001" }
        );
    }
}
