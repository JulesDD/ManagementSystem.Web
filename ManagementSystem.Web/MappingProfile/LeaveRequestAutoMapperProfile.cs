using AutoMapper;
using ManagementSystem.Web.Models.LeaveRequests;


namespace ManagementSystem.Web.MappingProfile;

public class LeaveRequestAutoMapperProfile : Profile
{
    public LeaveRequestAutoMapperProfile()
    {
        CreateMap<CreateLeaveRequestVM, LeaveRequest>();
    }
}
