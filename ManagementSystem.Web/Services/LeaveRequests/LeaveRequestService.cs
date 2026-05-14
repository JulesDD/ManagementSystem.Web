using AutoMapper;
using ManagementSystem.Web.Models.LeaveRequests;
using ManagementSystem.Web.Data;
using ManagementSystem.Web.MappingProfile;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Web.Models.LeaveQuotas;
using ManagementSystem.Web.Services.LeaveQuotas;
using ManagementSystem.Web.Services.Users;

namespace ManagementSystem.Web.Services.LeaveRequests;

public partial class LeaveRequestService(ApplicationDbContext _context, IMapper _mapper, IUsersService _usersService,ApplicationDbContext _dbContext, ILeaveQuotaService _leaveQuotaService) : ILeaveRequestService
{
    public async Task CancelLeaveRequest(int leaveRequestId)
    {
        var leaveRequest = await _dbContext.LeaveRequests.FindAsync(leaveRequestId);
        leaveRequest.LeaveStatusId = (int)LeaveRequestStatusEnum.Cancelled;

        //restore the employee's leave quota for the cancelled request
        await UpdateQuotaDays(leaveRequest, false);
        _dbContext.SaveChanges();

    }

    public async Task CreateLeaveRequest(CreateLeaveRequestVM model)
    {
        var leaveRequest = _mapper.Map<LeaveRequest>(model);
        var user = await _usersService.GetCurrentUser();
        leaveRequest.EmployeeId = user.Id;
        
        leaveRequest.LeaveStatusId = (int)LeaveRequestStatusEnum.Pending;
        _dbContext.Add(leaveRequest);

       await UpdateQuotaDays(leaveRequest, true);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<EmployeeLeaveRequestListVM> GetAllLeaveRequests()
    {
        var leaveRequests = await _dbContext.LeaveRequests
            .Include(lr => lr.LeaveType)
            .ToListAsync();

        var model = new EmployeeLeaveRequestListVM
        {
            ApprovedRequests = leaveRequests.Count(lr => lr.LeaveStatusId == (int)LeaveRequestStatusEnum.Approved),
            PendingRequests = leaveRequests.Count(lr => lr.LeaveStatusId == (int)LeaveRequestStatusEnum.Pending),
            RejectedRequests = leaveRequests.Count(lr => lr.LeaveStatusId == (int)LeaveRequestStatusEnum.Rejected),
            TotalRequests = leaveRequests.Count(),
            LeaveRequests = leaveRequests.Select(lr => new LeaveRequestListVM
            {
                Id = lr.Id,
                StartDate = lr.StartDate,
                EndDate = lr.EndDate,
                NumberOfDays = lr.EndDate.DayNumber - lr.StartDate.DayNumber,
                LeaveType = lr.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)lr.LeaveStatusId
            }).ToList()
        };

        return model;
    }

    public async Task<List<LeaveRequestListVM>> GetEmployeeLeaveRequests()
    {
        var user = await _usersService.GetCurrentUser();
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

    public async Task<ReviewLeaveRequestVM> GetLeaveRequestForReview(int id)
    {
        var leaveRequest = await _dbContext.LeaveRequests
            .Include(lr => lr.LeaveType)
            .FirstAsync(lr => lr.Id == id);
        var user = await _usersService.GetUserById(leaveRequest.EmployeeId);

        var model = new ReviewLeaveRequestVM
        {
            Id = leaveRequest.Id,
            StartDate = leaveRequest.StartDate,
            EndDate = leaveRequest.EndDate,
            LeaveType = leaveRequest.LeaveType.Name,
            NumberOfDays = leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber,
            LeaveRequestStatus = (LeaveRequestStatusEnum)leaveRequest.LeaveStatusId,
            RequestComments = leaveRequest.RequestComments,
            Employee = new EmployeeListVM
            {
                Id = leaveRequest.EmployeeId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            }
        };

        return model;
    }

    public async Task<bool> RequestDatesExceedQuota(CreateLeaveRequestVM model)
    {
        var user = await _usersService.GetCurrentUser();

        var currentDate = DateTime.Now;
        var period = await _dbContext.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
        var numberOfDays = model.EndDate.DayNumber - model.StartDate.DayNumber;
        var quotaToDeduct = await _dbContext.LeaveQuotas
            .FirstAsync(q => q.LeaveTypeId == model.LeaveTypeId && q.EmployeeId == user.Id && q.PeriodId == period.Id);

        return quotaToDeduct.NumberOfDays < numberOfDays;
    }

    public async Task ReviewLeaveRequest(int leaveRequestId, bool approved)
    {
        var user = await _usersService.GetCurrentUser();
        var leaveRequest = await _dbContext.LeaveRequests.FindAsync(leaveRequestId);
        leaveRequest.LeaveStatusId = approved ? (int)LeaveRequestStatusEnum.Approved : (int)LeaveRequestStatusEnum.Rejected;

        leaveRequest.ReviewerId = user.Id;
        if(!approved)
        {
            await UpdateQuotaDays(leaveRequest, false);
        }
        await _dbContext.SaveChangesAsync();
    }

    private async Task UpdateQuotaDays(LeaveRequest leaveRequest, bool deductDays)
    {
        var quota = await _leaveQuotaService.GetCurrentQuota(leaveRequest.LeaveTypeId, leaveRequest.EmployeeId);
        var numberOfDays = CalculateDays(leaveRequest.StartDate, leaveRequest.EndDate);

        if(deductDays)
        {
            quota.NumberOfDays -= numberOfDays;
        }
        else
        {
            quota.NumberOfDays += numberOfDays;
        }

        _context.Entry(quota).State = EntityState.Modified;
    }

    private int CalculateDays(DateOnly startDate, DateOnly endDate)
    {
        return endDate.DayNumber - startDate.DayNumber;
    }
}
