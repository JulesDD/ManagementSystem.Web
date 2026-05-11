using ManagementSystem.Web.Models.LeaveQuotas;
using ManagementSystem.Web.Services.LeaveQuotas;
using ManagementSystem.Web.Services.LeaveTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.Web.Controllers;

//Create a controller for leave quotas from scratch, with an index action that returns a view.
//The controller should be named LeaveQuotaController and should use the ILeaveQuotaService to get the leave quotas from the database.
//The index action should return a view that displays the leave quotas in a table.
[Authorize]
public class LeaveQuotaController(ILeaveQuotaService _leaveQuotaService, ILeaveTypeService _leaveTypeService) : Controller
{
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Index()
    {
        var employeeVM = await _leaveQuotaService.GetEmployees();
        return View(employeeVM);
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> QuotaLeave(string? id)
    {
        await _leaveQuotaService.QuotaLeave(id);
        return RedirectToAction(nameof(Details), new { userId = id });
    }

    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> EditQuota(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var employeeVM = await _leaveQuotaService.GetEmployeeQuota(id.Value);
        if (employeeVM == null)
        {
            return NotFound();
        }
        return View(employeeVM);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditQuota(LeaveQuotaEditVM leaveQuotaEditVM)
    {
        if (await _leaveTypeService.DaysExceedLimit(leaveQuotaEditVM.NumberOfDays, leaveQuotaEditVM.LeaveType.Id))
        {
            ModelState.AddModelError("NumberOfDays", "The number of days exceeds the limit for this leave type.");
        }
        if(ModelState.IsValid)
        {
            await _leaveQuotaService.EditEmployeeQuota(leaveQuotaEditVM);
            return RedirectToAction(nameof(Details), new { userId = leaveQuotaEditVM.Employee.Id });
        }

        var days = leaveQuotaEditVM.NumberOfDays;
        leaveQuotaEditVM = await _leaveQuotaService.GetEmployeeQuota(leaveQuotaEditVM.Id);
        leaveQuotaEditVM.NumberOfDays = days;
        return View(leaveQuotaEditVM);      
    }

    public async Task<IActionResult> Details(string? userId)
    {
        var employeeVM = await _leaveQuotaService.GetEmployeeQuotas(userId);
        return View(employeeVM);
    }
}
