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
    public async Task CancelLeaveRequest(int leaveRequestId)
    {
        var leaveRequest = await _dbContext.LeaveRequests.FindAsync(leaveRequestId);
        leaveRequest.LeaveStatusId = (int)LeaveRequestStatusEnum.Cancelled;

        //restore the employee's leave quota for the cancelled request
        var numberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber;
        var quotaToRestore = await _dbContext.LeaveQuotas
            .FirstAsync(q => q.LeaveTypeId == leaveRequest.LeaveTypeId && q.EmployeeId == leaveRequest.EmployeeId);

        quotaToRestore.NumberOfDays += numberOfDays;

       _dbContext.SaveChanges();

    }

    public async Task CreateLeaveRequest(CreateLeaveRequestVM model)
    {
        var leaveRequest = _mapper.Map<LeaveRequest>(model);
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        leaveRequest.EmployeeId = user.Id;
        
        leaveRequest.LeaveStatusId = (int)LeaveRequestStatusEnum.Pending;
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

    public async Task<List<LeaveRequestListVM>> GetEmployeeLeaveRequests()
    {
        var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        var leaveRequests = await _dbContext.LeaveRequests
            .Include(lr => lr.LeaveType)
            .Where(lr => lr.EmployeeId == user.Id)
            .ToListAsync();

        var model = leaveRequests.Select(lr => new LeaveRequestListVM
        {
            Id = lr.Id,
            StartDate = lr.StartDate,
            EndDate = lr.EndDate,
            NumberOfDays = lr.EndDate.DayNumber - lr.StartDate.DayNumber,
            LeaveType = lr.LeaveType.Name,
            LeaveRequestStatus = (LeaveRequestStatusEnum)lr.LeaveStatusId
        }).ToList();

        return model;
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
