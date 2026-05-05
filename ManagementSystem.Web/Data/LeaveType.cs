using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Web.Data;

public class LeaveType : BaseEntity
{
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;
    public int NumberOfDays { get; set; } = 0;
}
