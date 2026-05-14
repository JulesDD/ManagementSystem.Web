using ManagementSystem.Web.Models.LeaveQuotas;

namespace ManagementSystem.Web.Services.LeaveQuotas;

public interface ILeaveQuotaService
{
    Task QuotaLeave(string EmployeeId);
    Task<List<LeaveQuota>> GetQuota(string? userId);
    Task<EmployeeQuotaVM> GetEmployeeQuotas(string? userId);
    Task<LeaveQuotaEditVM> GetEmployeeQuota(int? quotaId);

    Task<List<EmployeeListVM>> GetEmployees();
    Task EditEmployeeQuota(LeaveQuotaEditVM leaveQuotaEditVM);

    Task<LeaveQuota> GetCurrentQuota(int leaveTypeId, string employeeId);
}