using AutoMapper;
using ManagementSystem.Web.Models.LeaveQuotas;
using ManagementSystem.Web.Models.LeaveTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Web.Services.LeaveQuotas;

public class LeaveQuotaService(ApplicationDbContext _context, IHttpContextAccessor _httpContextAccessor, UserManager<ApplicationUser> _userManager, IMapper _mapper) : ILeaveQuotaService
{
    // this method will be called when an employee is registered to calculate the leave quota for the employee based on the leave types and the current period
    // the leave quota will be calculated based on the number of months left in the year and the number of days for each leave type
    public async Task QuotaLeave(string employeeId)
    {
        //get all leave types
        var leaveTypes = await _context.LeaveTypes.Where(q => !q.LeaveQuotas.Any(x => x.EmployeeId == employeeId)).ToListAsync();

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
                EmployeeId = employeeId,
                LeaveTypeId = leaveType.Id,
                PeriodId = period.Id,
                NumberOfDays = (int)Math.Ceiling(accuralRate * monthsRemaining)
            };
            _context.Add(leaveQuota);
        }
        await _context.SaveChangesAsync();
    }

    // get the leave quotas for the current employee
    // the leave quotas should include the leave type and the number of days
    public async Task<List<LeaveQuota>> GetQuota(string? userId)
    {
        var currentDate = DateTime.Now;
        var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);

        var leaveQuotas = await _context.LeaveQuotas
            .Include(q => q.LeaveType)
            .Include(q => q.Period)
            .Where(q => q.EmployeeId == userId && q.PeriodId == period.Id)
            .ToListAsync();

        return leaveQuotas;
    }

    // get the leave quotas for the current employee and return them in a view model
    // the view model should contain the employee's name, email, and a list of leave quotas with the leave type and number of days
    public async Task<EmployeeQuotaVM> GetEmployeeQuotas(string? userId)
    {
        var user = string.IsNullOrEmpty(userId) ? await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User) : await _userManager.FindByIdAsync(userId);
        var quotas = await GetQuota(user.Id);
        var quotaList = _mapper.Map<List<LeaveQuota>, List<LeaveQuotaVM>>(quotas);
        var leaveTypesCount = await _context.LeaveTypes.CountAsync();

        var employeeQuotaVM = new EmployeeQuotaVM
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DateOfBirth,
            Email = user.Email,
            LeaveQuotas = quotaList,
            IsQuotaEmpty = leaveTypesCount == quotas.Count
        };
        return employeeQuotaVM;
    }

    // get the leave quotas for all employees and return them in a view model
    public async Task<List<EmployeeListVM>> GetEmployees()
    {
        var employees = await _userManager.GetUsersInRoleAsync("Employee");
        var employeeList = _mapper.Map<List<ApplicationUser>, List<EmployeeListVM>>(employees.ToList());

        return employeeList;
    }

    // get the leave quota for a specific employee and return it in a view model
    public async Task<LeaveQuotaEditVM> GetEmployeeQuota(int? quotaId)
    {
        var quota = await _context.LeaveQuotas
            .Include(q => q.LeaveType)
            .Include(q => q.Employee)
            .FirstOrDefaultAsync(q => q.Id == quotaId);

        var model = _mapper.Map<LeaveQuotaEditVM>(quota);

        return model;

    }

    // edit the leave quota for a specific employee
    // the edit should only allow changing the number of days for the leave quota
    public async Task EditEmployeeQuota(LeaveQuotaEditVM leaveQuotaEditVM)
    {
        //var quota = await GetEmployeeQuota(leaveQuotaEditVM.Id) ?? throw new Exception("Leave quota not found"); 
        //quota.NumberOfDays = leaveQuotaEditVM.NumberOfDays;
        //_context.Entry(quota).State = EntityState.Modified;
        //await _context.SaveChangesAsync();

        await _context.LeaveQuotas
            .Where(q => q.Id == leaveQuotaEditVM.Id)
            .ExecuteUpdateAsync(s => s.SetProperty( d => d.NumberOfDays, leaveQuotaEditVM.NumberOfDays ));
    }

    // check if the leave quota for a specific employee and leave type already exists for the current period
    private async Task<bool> QuotaExist(string userId, int periodId, int leaveTypeId)
    {
        return await _context.LeaveQuotas.AnyAsync(q => q.EmployeeId == userId && q.PeriodId == periodId && q.LeaveTypeId == leaveTypeId);
    }
}
