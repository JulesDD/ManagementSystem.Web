using AutoMapper;
using ManagementSystem.Web.Data;
using ManagementSystem.Web.Models.LeaveTypes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace ManagementSystem.Web.Services;

public class LeaveTypeService(ApplicationDbContext _context, IMapper _mapper) : ILeaveTypeService
{

    public async Task<List<LeaveTypeIndexVM>> GetAllLeaveTypes()
    {
        var leaveTypes = await _context.LeaveTypes.ToListAsync();
        var leaveTypeVMs = _mapper.Map<List<LeaveTypeIndexVM>>(leaveTypes);
        return leaveTypeVMs;
    }

    public async Task<T?> Get<T>(int id) where T : class
    {
        var leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(lt => lt.LeaveTypeId == id);
        if (leaveType == null)
        {
            return null;
        }
        return _mapper.Map<T>(leaveType);
    }

    public async Task Delete(int id)
    {
        var leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(lt => lt.LeaveTypeId == id);
        if (leaveType != null)
        {
            _context.LeaveTypes.Remove(leaveType);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Create(CreateLeaveTypeVM model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.Add(leaveType);
        await _context.SaveChangesAsync();
    }

    public async Task Update(EditLeaveTypeVM model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.Update(leaveType);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckIfLeaveTypeNameExists(string name)
    {
        var lowercaseName = name.ToLower();
        return await _context.LeaveTypes.AnyAsync(lt => lt.Name.ToLower().Equals(lowercaseName));
    }

    public async Task<bool> CheckIfLeaveTypeNameExistsForEdit(EditLeaveTypeVM editLeaveTypeVM)
    {
        var lowercaseName = editLeaveTypeVM.Name.ToLower();
        return await _context.LeaveTypes.AnyAsync(lt => lt.Name.ToLower().Equals(lowercaseName) && lt.LeaveTypeId != editLeaveTypeVM.LeaveTypeId);
    }

    public bool LeaveTypeExists(int id)
    {
        return _context.LeaveTypes.Any(e => e.LeaveTypeId == id);
    }

}
