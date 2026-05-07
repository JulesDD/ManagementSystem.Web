using AutoMapper;
using ManagementSystem.Web.Models.LeaveQuotas;
using ManagementSystem.Web.Models.LeaveTypes;
using ManagementSystem.Web.Models.Periods;

namespace ManagementSystem.Web.MappingProfile;

public class LeaveTypeAutoMapperProfile : Profile
{
    public LeaveTypeAutoMapperProfile()
    {
        CreateMap<LeaveQuota, LeaveQuotaVM>();
        CreateMap<Period, PeriodVM>();
    }
}
