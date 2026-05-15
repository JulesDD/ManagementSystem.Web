using ManagementSystem.Application.Models.LeaveTypes;
using ManagementSystem.Application.Models.Periods;
using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Application.Models.LeaveQuotas;

public class LeaveQuotaVM
{
    public int Id { get; set; }
    [Display(Name = "Number of Days")]
    public int NumberOfDays { get; set; }

    [Display(Name = "Leave Period")]
    public PeriodVM Period { get; set; } = new PeriodVM();
    public LeaveTypeIndexVM LeaveType { get; set; } = new LeaveTypeIndexVM();
}
