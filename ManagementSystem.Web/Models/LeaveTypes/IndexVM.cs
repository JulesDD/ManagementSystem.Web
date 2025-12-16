namespace ManagementSystem.Web.Models.LeaveTypes;


public class LeaveTypeIndexVM
{
    public int LeaveTypeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int NumberOfDays { get; set; }
}
