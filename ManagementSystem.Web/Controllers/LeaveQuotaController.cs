using ManagementSystem.Web.Services.LeaveQuotas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.Web.Controllers;

//Create a controller for leave quotas from scratch, with an index action that returns a view.
//The controller should be named LeaveQuotaController and should use the ILeaveQuotaService to get the leave quotas from the database.
//The index action should return a view that displays the leave quotas in a table.
[Authorize]
public class LeaveQuotaController(ILeaveQuotaService _leaveQuotaService) : Controller
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

    public async Task<IActionResult> Details(string? userId)
    {
        var employeeVM = await _leaveQuotaService.GetEmployeeQuotas(userId);
        return View(employeeVM);
    }
}
