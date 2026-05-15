namespace ManagementSystem.Application.Services.LeaveQuotas
{
    public interface ILeaveQuotaService1
    {
        Task EditEmployeeQuota(LeaveQuotaEditVM leaveQuotaEditVM);
        Task<LeaveQuotaVM> GetCurrentQuota(int leaveTypeId, string employeeId);
        Task<LeaveQuotaEditVM> GetEmployeeQuota(int? quotaId);
        Task<EmployeeQuotaVM> GetEmployeeQuotas(string? userId);
        Task<List<EmployeeListVM>> GetEmployees();
        Task<List<LeaveQuotaVM>> GetQuota(string? userId);
        Task QuotaLeave(string EmployeeId);
    }
}