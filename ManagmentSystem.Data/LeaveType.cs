using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Data;

public class LeaveType : BaseEntity
{
    [MaxLength(150)]
    public string Name { get; set; }
    public int NumberOfDays { get; set; }

    public List<LeaveQuota>? LeaveQuotas { get; set; }
}
