using ManagementSystem.Web.Models.LeaveTypes;

namespace ManagementSystem.Web.Services
{
    public interface ILeaveTypeService
    {
        Task<bool> CheckIfLeaveTypeNameExists(string name);
        Task<bool> CheckIfLeaveTypeNameExistsForEdit(EditLeaveTypeVM editLeaveTypeVM);
        Task Create(CreateLeaveTypeVM model);
        Task Delete(int id);
        Task<T?> Get<T>(int id) where T : class;
        Task<List<LeaveTypeIndexVM>> GetAllLeaveTypes();
        bool LeaveTypeExists(int id);
        Task Update(EditLeaveTypeVM model);
    }
}