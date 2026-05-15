using ManagementSystem.Data.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ManagementSystem.Data;

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
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveQuota> LeaveQuotas { get; set; }
    public DbSet<Period> Periods { get; set; }

    public DbSet<LeaveRequest> LeaveRequests { get; set; }
    public DbSet<LeaveRequestStatus> LeaveRequestStatuses { get; set; }

}
