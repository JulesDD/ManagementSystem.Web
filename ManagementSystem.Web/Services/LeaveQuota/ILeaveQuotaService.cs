namespace ManagementSystem.Web.Services.LeaveQuotas;

public interface ILeaveQuotaService
{
    Task QuotaLeave(string EmployeeId);
}