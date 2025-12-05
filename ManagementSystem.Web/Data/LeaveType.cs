using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Web.Data;

public class LeaveType
{
    public int leaveTypeId { get; set; }
    [Column(TypeName = "nvarchar(150)")]
    public string Name { get; set; } = string.Empty;
    public int NumberOfDays { get; set; } = 0;
}
