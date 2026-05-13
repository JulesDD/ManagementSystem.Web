using AutoMapper;
using ManagementSystem.Web.Models.LeaveRequests;
using ManagementSystem.Web.Data;
using ManagementSystem.Web.MappingProfile;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Web.Services.LeaveRequests;

public partial class LeaveRequestService(ApplicationDbContext _context, IMapper _mapper, UserManager<ApplicationUser> _userManager, IHttpContextAccessor _httpContextAccessor,
    ApplicationDbContext _dbContext) : ILeaveRequestService
{
    public Task CancelLeaveRequest(int leaveRequestId)
    {
        throw new NotImplementedException();
    }

    public async Task CreateLeaveRequest(CreateLeaveRequestVM model)
    {
        var leaveRequest = _mapper.Map<LeaveRequest>(model);
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        leaveRequest.EmployeeId = user.Id;
        
        leaveRequest.LeaveStatusId = (int)LeaveRequestStatus.Pending;
        _dbContext.Add(leaveRequest);

        var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
        var quotaToDeduct = await _dbContext.LeaveQuotas
            .FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == user.Id);

        quotaToDeduct.NumberOfDays -= numberOfDays;
        await _dbContext.SaveChangesAsync();
    }

    public Task<LeaveRequestListVM> GetAllLeaveRequests()
    {
        throw new NotImplementedException();
    }

    public Task<EmployeeLeaveRequestListVM> GetEmployeeLeaveRequests(string employeeId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RequestDatesExceedQuota(CreateLeaveRequestVM model)
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
        var quotaToDeduct = await _dbContext.LeaveQuotas
            .FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == user.Id);

        return quotaToDeduct.NumberOfDays < numberOfDays;
    }

    public Task ReviewLeaveRequest(ReviewLeaveRequestVM model)
    {
        throw new NotImplementedException();
    }
}
