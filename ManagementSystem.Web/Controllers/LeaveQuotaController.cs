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


    public async Task<IActionResult> Index()
    {
        var leaveQuota = await _leaveQuotaService.GetQuota();
        return View();
    }
}
