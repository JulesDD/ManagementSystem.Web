using ManagementSystem.Application.Models.LeaveTypes;

namespace ManagementSystem.Application.Services.LeaveTypes
{
    public interface ILeaveTypeService
    {
        Task<bool> CheckIfLeaveTypeNameExists(string name);
        Task<bool> CheckIfLeaveTypeNameExistsForEdit(EditLeaveTypeVM editLeaveTypeVM);
        Task Create(CreateLeaveTypeVM model);
        Task<bool> DaysExceedLimit(int numberOfDays, int leaveTypeId);
        Task Delete(int id);
        Task<T?> Get<T>(int id) where T : class;
        Task<List<LeaveTypeIndexVM>> GetAllLeaveTypes();
        bool LeaveTypeExists(int id);
        Task Update(EditLeaveTypeVM model);
    }
}