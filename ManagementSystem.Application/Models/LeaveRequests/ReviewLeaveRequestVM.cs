using ManagementSystem.Application.Models.LeaveQuotas;
using System.ComponentModel;

namespace ManagementSystem.Application.Models.LeaveRequests;

public class ReviewLeaveRequestVM : LeaveRequestListVM
{
    public EmployeeListVM Employee { get; set; } = new EmployeeListVM();

    [DisplayName("Additional Comments")]
    public string RequestComments { get; set; } = string.Empty;
}