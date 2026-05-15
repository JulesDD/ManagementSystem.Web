using AutoMapper;
using ManagementSystem.Application.Models.LeaveRequests;

namespace ManagementSystem.Application.MappingProfile;

public class LeaveRequestAutoMapperProfile : Profile
{
    public LeaveRequestAutoMapperProfile()
    {
        CreateMap<CreateLeaveRequestVM, LeaveRequest>();
    }
}
