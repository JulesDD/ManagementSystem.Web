using AutoMapper;
using ManagementSystem.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Web.Services.LeaveQuotas;

public class LeaveQuotaService(ApplicationDbContext _context) : ILeaveQuotaService
{
    public async Task QuotaLeave(string EmployeeId)
    {
        //get all leave types
        var leaveTypes = await _context.LeaveTypes.ToListAsync();

        //get the current period based on year
        var currentDate = DateTime.Now;
        var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
        var monthsRemaining = period.EndDate.Month - currentDate.Month;

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
}
