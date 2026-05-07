using ManagementSystem.Web.Models.LeaveTypes;
using ManagementSystem.Web.Models.Periods;
using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Web.Models.LeaveQuotas;

public class LeaveQuotaVM
{
    public int Id { get; set; }
    [Display(Name = "Number of Days")]
    public int NumberOfDays { get; set; }

    [Display(Name = "Leave Period")]
    public PeriodVM LeavePeriod { get; set; } = new PeriodVM();
    public BaseLeaveTypeVM LeaveType { get; set; } = new BaseLeaveTypeVM();
}
