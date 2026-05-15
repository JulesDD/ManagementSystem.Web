using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Data;

public class LeaveRequestStatus : BaseEntity
{
    [StringLength(40)]
    public string Name { get; set; }
}