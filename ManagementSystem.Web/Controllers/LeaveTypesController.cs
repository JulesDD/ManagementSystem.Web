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

namespace ManagementSystem.Web.Controllers;

public class LeaveTypesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public LeaveTypesController(ApplicationDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: LeaveTypes
    // Get all leave types
    public async Task<IActionResult> Index()
    {
        var data = await _context.LeaveTypes.ToListAsync();

        //convert to datamodel into viewmodel using AutoMapper
        var dataVm = _mapper.Map<List<LeaveTypeIndexVM>>(data);

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

        var leaveType = await _context.LeaveTypes
            .FirstOrDefaultAsync(m => m.LeaveTypeId == id);

        if (leaveType == null)
        {
            return NotFound();
        }

        //Map data model to view model
        var leaveTypeVm = _mapper.Map<LeaveTypeIndexVM>(leaveType);
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
        if(await CheckIfLeaveTypeNameExists(leaveTypeCreateVM.Name))
        {
            ModelState.AddModelError(nameof(leaveTypeCreateVM.Name), "Leave type with this name already exists.");
        }

        if (ModelState.IsValid)
        {
            var leaveType = _mapper.Map<LeaveType>(leaveTypeCreateVM);
            _context.Add(leaveType);
            await _context.SaveChangesAsync();
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

        var leaveType = await _context.LeaveTypes.FindAsync(id);
        if (leaveType == null)
        {
            return NotFound();
        }

        var leaveTypeVm = _mapper.Map<EditLeaveTypeVM>(leaveType);

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

        if (await CheckIfLeaveTypeNameExistsForEdit(editLeaveTypeVM))
        {
            ModelState.AddModelError(nameof(editLeaveTypeVM.Name), "Leave type with this name already exists.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                var leaveTypeVm = _mapper.Map<LeaveType>(editLeaveTypeVM);
                _context.Update(leaveTypeVm);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LeaveTypeExists(editLeaveTypeVM.LeaveTypeId))
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

        var leaveType = await _context.LeaveTypes
            .FirstOrDefaultAsync(m => m.LeaveTypeId == id);
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
        var leaveType = await _context.LeaveTypes.FindAsync(id);
        if (leaveType != null)
        {
            _context.LeaveTypes.Remove(leaveType);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> CheckIfLeaveTypeNameExists(string name)
    {
        var lowercaseName = name.ToLower();
        return await _context.LeaveTypes.AnyAsync(lt => lt.Name.ToLower().Equals(lowercaseName));
    }

    private async Task<bool> CheckIfLeaveTypeNameExistsForEdit(EditLeaveTypeVM editLeaveTypeVM)
    {
        var lowercaseName = editLeaveTypeVM.Name.ToLower();
        return await _context.LeaveTypes.AnyAsync(lt => lt.Name.ToLower().Equals(lowercaseName) && lt.LeaveTypeId != editLeaveTypeVM.LeaveTypeId);
    }

    private bool LeaveTypeExists(int id)
    {
        return _context.LeaveTypes.Any(e => e.LeaveTypeId == id);
    }
}