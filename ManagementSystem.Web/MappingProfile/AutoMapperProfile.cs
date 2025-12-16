using AutoMapper;
using ManagementSystem.Web.Data;
using ManagementSystem.Web.Models.LeaveTypes;

namespace ManagementSystem.Web.MappingProfile;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LeaveType, LeaveTypeIndexVM>();
    }
}
