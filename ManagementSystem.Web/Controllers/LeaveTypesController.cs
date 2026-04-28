using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Web.Data;
using ManagementSystem.Web.Models.LeaveTypes;
using AutoMapper;
using ManagementSystem.Web.Services;

namespace ManagementSystem.Web.Controllers;

public class LeaveTypesController(ILeaveTypeService _leaveTypeService) : Controller
{

    // GET: LeaveTypes
    // Get all leave types
    public async Task<IActionResult> Index()
    {
        var dataVm = await _leaveTypeService.GetAllLeaveTypes();
        //return the view with VM
        return View(dataVm);
    }

    // GET: LeaveTypes/Details/5
    // Get details of a specific leave type by id
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaveType =  await _leaveTypeService.Get<LeaveTypeIndexVM>(id.Value);

        if (leaveType == null)
        {
            return NotFound();
        }

        return View(leaveType);
    }

    // GET: LeaveTypes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: LeaveTypes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateLeaveTypeVM leaveTypeCreateVM)
    {
        if(await _leaveTypeService.CheckIfLeaveTypeNameExists(leaveTypeCreateVM.Name))
        {
            ModelState.AddModelError(nameof(leaveTypeCreateVM.Name), "Leave type with this name already exists.");
        }

        if (ModelState.IsValid)
        {
            await _leaveTypeService.Create(leaveTypeCreateVM);
            return RedirectToAction(nameof(Index));
        }
        return View(leaveTypeCreateVM);
    }

    // GET: LeaveTypes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaveTypeVm = await _leaveTypeService.Get<EditLeaveTypeVM>(id.Value);
        if (leaveTypeVm == null)
        {
            return NotFound();
        }


        return View(leaveTypeVm);
    }

    // POST: LeaveTypes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, EditLeaveTypeVM editLeaveTypeVM)
    {
        if (id != editLeaveTypeVM.LeaveTypeId)
        {
            return NotFound();
        }

        if (await _leaveTypeService.CheckIfLeaveTypeNameExistsForEdit(editLeaveTypeVM))
        {
            ModelState.AddModelError(nameof(editLeaveTypeVM.Name), "Leave type with this name already exists.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _leaveTypeService.Update(editLeaveTypeVM);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_leaveTypeService.LeaveTypeExists(editLeaveTypeVM.LeaveTypeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(editLeaveTypeVM);
    }

    // GET: LeaveTypes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var leaveType = await _leaveTypeService.Get<LeaveTypeIndexVM>(id.Value);
        if (leaveType == null)
        {
            return NotFound();
        }

        return View(leaveType);
    }

    // POST: LeaveTypes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _leaveTypeService.Delete(id);
        return RedirectToAction(nameof(Index));
    }

   
}