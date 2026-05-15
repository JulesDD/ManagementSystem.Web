namespace ManagementSystem.Application.Services.LeaveRequests;

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