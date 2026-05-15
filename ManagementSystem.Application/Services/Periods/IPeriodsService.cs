namespace ManagementSystem.Application.Services.Periods;

public interface IPeriodsService
{
    Task<Period> GetCurrentPeriod();
}