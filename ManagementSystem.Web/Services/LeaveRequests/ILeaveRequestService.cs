using ManagementSystem.Web.Models.LeaveQuotas;
using ManagementSystem.Web.Models.LeaveRequests;

namespace ManagementSystem.Web.Services.LeaveRequests;

public interface ILeaveRequestService
{  
    Task CreateLeaveRequest(CreateLeaveRequestVM model);
    Task<List<LeaveRequestListVM>> GetEmployeeLeaveRequests();
    Task<EmployeeLeaveRequestListVM> GetAllLeaveRequests();
    Task CancelLeaveRequest(int leaveRequestId);
    Task ReviewLeaveRequest(int leaveRequestId, bool approved);
    Task<bool> RequestDatesExceedQuota(CreateLeaveRequestVM model);
    Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(int id);
}