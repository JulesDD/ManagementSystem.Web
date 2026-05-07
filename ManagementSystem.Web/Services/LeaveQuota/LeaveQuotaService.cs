using AutoMapper;
using ManagementSystem.Web.Models.LeaveTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Web.Services.LeaveQuotas;

public class LeaveQuotaService(ApplicationDbContext _context, IHttpContextAccessor _httpContextAccessor, UserManager<ApplicationUser> _userManager) : ILeaveQuotaService
{
    public async Task QuotaLeave(string EmployeeId)
    {
        //get all leave types
        var leaveTypes = await _context.LeaveTypes.ToListAsync();

        //get the current period based on year
        var currentDate = DateTime.Now;
        var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
        var monthsRemaining = period.EndDate.Month - currentDate.Month;

        if (period == null)
        {
            throw new InvalidOperationException(
                $"Registration failed: No period configured for year {currentDate.Year}");
        }

        //for each leave type, check if there is a leave quota for the employee for the current period
        //calculate the leave based on number of months left in the year
        foreach (var leaveType in leaveTypes)
        {
            var accuralRate = decimal.Divide(leaveType.NumberOfDays, 12);
            var leaveQuota = new LeaveQuota
            {
                EmployeeId = EmployeeId,
                LeaveTypeId = leaveType.Id,
                PeriodId = period.Id,
                NumberOfDays = (int)Math.Ceiling(accuralRate * monthsRemaining)
            };
            _context.Add(leaveQuota);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<List<LeaveQuota>> GetQuota()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);

        var leaveQuotas = await _context.LeaveQuotas
            .Include(q => q.LeaveType)
            .Include(q => q.Period)
            .Include(q => q.Employee)
            .Where(q => q.EmployeeId == user.Id)
            .ToListAsync();


        return leaveQuotas;
    }
}
