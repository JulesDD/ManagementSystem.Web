using AutoMapper;

namespace ManagementSystem.Application.MappingProfile;

public class LeaveTypeAutoMapperProfile : Profile
{
    public LeaveTypeAutoMapperProfile()
    {
        CreateMap<LeaveQuota, LeaveQuotaVM>();
        CreateMap<LeaveQuota, LeaveQuotaEditVM>();
        CreateMap<ApplicationUser, EmployeeListVM>();
        CreateMap<Period, PeriodVM>();
    }
}
