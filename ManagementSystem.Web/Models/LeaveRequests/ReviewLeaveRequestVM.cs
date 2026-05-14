using ManagementSystem.Web.Models.LeaveQuotas;
using System.ComponentModel;

namespace ManagementSystem.Web.Models.LeaveRequests;

public class ReviewLeaveRequestVM : LeaveRequestListVM
{
    public EmployeeListVM Employee { get; set; } = new EmployeeListVM();

    [DisplayName("Additional Comments")]
    public string RequestComments { get; set; } = string.Empty;
}