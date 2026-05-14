using ManagementSystem.Web.Models.LeaveRequests;
using ManagementSystem.Web.Services.LeaveRequests;
using ManagementSystem.Web.Services.LeaveTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManagementSystem.Web.Controllers;

[Authorize]
public class LeaveRequestController(ILeaveTypeService _leaveTypeService, ILeaveRequestService _leaveRequestService) : Controller
{
    //Employee can view their leave requests
    public async Task<IActionResult> Index()
    {
        var model = await _leaveRequestService.GetEmployeeLeaveRequests();
        return View(model);
    }

    // Employee can create a new leave request
    // GET: LeaveRequest/Create
    public async Task<IActionResult> Create()
    {
        var leaveTypes = await _leaveTypeService.GetAllLeaveTypes();
        var selectLeaveTypes = new SelectList(leaveTypes, "Id", "Name");
        var model = new CreateLeaveRequestVM
        {
            StartDate = DateOnly.FromDateTime(DateTime.Now),
            EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            LeaveTypes = selectLeaveTypes,
            RequestComments = string.Empty
        };
        return View(model);
    }

    // POST: LeaveRequest/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateLeaveRequestVM model)
    {
        // validate the days do not exceed the employee's available leave days for the selected leave type
        if(await _leaveRequestService.RequestDatesExceedQuota(model))
        {
            ModelState.AddModelError(string.Empty, "Unfortunately, your leave request cannot be submitted.");
            ModelState.AddModelError(nameof(model.EndDate),"The number of days requested exceeds your available leave days for the selected leave type. Please adjust your request or speak to your supervisor to request more time.");
        }

        if (ModelState.IsValid)
        {
            await _leaveRequestService.CreateLeaveRequest(model);
            return RedirectToAction(nameof(Index));
        }
        var leaveTypes = await _leaveTypeService.GetAllLeaveTypes();
        model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");
        return View(model);
    }

    //Employee can cancel a pending leave request
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cancel(int id)
    {
        await _leaveRequestService.CancelLeaveRequest(id);
        return RedirectToAction(nameof(Index));
    }

    //Admin can view all leave requests
    public async Task<IActionResult> ListRequests()
    {
        var model = await _leaveRequestService.GetAllLeaveRequests();
        return View(model);
    }

    public async Task<IActionResult> Review(int id)
    {   
        var model = await _leaveRequestService.GetLeaveRequestForReview(id);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Review(int id, bool approved)
    {
        await _leaveRequestService.ReviewLeaveRequest(id, approved);
        return RedirectToAction(nameof(ListRequests));
    }
}
