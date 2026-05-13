namespace ManagementSystem.Web.Services.LeaveRequests;

public partial class LeaveRequestService
{
    public enum LeaveRequestStatus
    {
        Pending = 1,
        Approved =2,
        Rejected = 3,
        Cancelled = 4
    }
}
