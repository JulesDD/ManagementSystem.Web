using AutoMapper;
using ManagementSystem.Application.Models.LeaveTypes;
using ManagementSystem.Data;

namespace ManagementSystem.Application.MappingProfile;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<LeaveType, LeaveTypeIndexVM>();
        CreateMap<CreateLeaveTypeVM, LeaveType>();
        CreateMap<EditLeaveTypeVM, LeaveType>().ReverseMap();
    }
}
