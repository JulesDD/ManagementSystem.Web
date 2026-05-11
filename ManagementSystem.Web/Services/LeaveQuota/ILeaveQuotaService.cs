using ManagementSystem.Web.Models.LeaveQuotas;

namespace ManagementSystem.Web.Services.LeaveQuotas;

public interface ILeaveQuotaService
{
    Task QuotaLeave(string EmployeeId);
    Task<List<LeaveQuota>> GetQuota(string? userId);
    Task<EmployeeQuotaVM> GetEmployeeQuotas(string? userId);

    Task<List<EmployeeListVM>> GetEmployees();
}