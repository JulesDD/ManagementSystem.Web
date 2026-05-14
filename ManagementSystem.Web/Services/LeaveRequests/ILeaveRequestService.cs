using ManagementSystem.Web.Models.LeaveQuotas;
using ManagementSystem.Web.Models.LeaveRequests;

namespace ManagementSystem.Web.Services.LeaveRequests;

public interface ILeaveRequestService
{
    Task CreateLeaveRequest(CreateLeaveRequestVM model);
    Task<List<LeaveRequestListVM>> GetEmployeeLeaveRequests();
    Task<LeaveRequestListVM> GetAllLeaveRequests();
    Task CancelLeaveRequest(int leaveRequestId);
    Task ReviewLeaveRequest(ReviewLeaveRequestVM model);
    Task<bool> RequestDatesExceedQuota(CreateLeaveRequestVM model);
}