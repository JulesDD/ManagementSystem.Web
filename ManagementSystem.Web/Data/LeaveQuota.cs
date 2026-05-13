namespace ManagementSystem.Web.Data;

// LeaveAllocation entity to represent the leave quota for each employee
// LeaveAllocation == LeaveQuota
public class LeaveQuota : BaseEntity
{
    // Navigation properties for LeaveType
    public LeaveType? LeaveType { get; set; }
    public int LeaveTypeId { get; set; }

    // Navigation properties for Employee
    public ApplicationUser? Employee { get; set; }
    public string? EmployeeId { get; set; }
    
    // Navigation properties for Period
    public Period? Period { get; set; }
    public int PeriodId { get; set; }

    // Number of days allocated for the leave type in the given period
    public int NumberOfDays { get; set; }
}
