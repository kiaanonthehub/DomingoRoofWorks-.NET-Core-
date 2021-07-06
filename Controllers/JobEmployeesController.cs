using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomingoRoofWorks.Models;

namespace DomingoRoofWorks.Controllers
{
    public class JobEmployeesController : Controller
    {
        private readonly Domingo_Roof_WorksContext _context;

        public JobEmployeesController(Domingo_Roof_WorksContext context)
        {
            _context = context;
        }

        // GET: JobEmployees
        public async Task<IActionResult> Index()
        {
            var domingo_Roof_WorksContext = _context.JobEmployees.Include(j => j.Employee).Include(j => j.JobCard);
            return View(await domingo_Roof_WorksContext.ToListAsync());
        }

        // GET: JobEmployees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobEmployee = await _context.JobEmployees
                .Include(j => j.Employee)
                .Include(j => j.JobCard)
                .FirstOrDefaultAsync(m => m.JobEmployeeId == id);
            if (jobEmployee == null)
            {
                return NotFound();
            }

            return View(jobEmployee);
        }

        // GET: JobEmployees/Create
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            ViewData["JobCardId"] = new SelectList(_context.Jobs, "JobCardId", "JobCardId");
            return View();
        }

        // POST: JobEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobEmployeeId,JobCardId,EmployeeId")] JobEmployee jobEmployee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", jobEmployee.EmployeeId);
            ViewData["JobCardId"] = new SelectList(_context.Jobs, "JobCardId", "JobCardId", jobEmployee.JobCardId);
            return View(jobEmployee);
        }

        // GET: JobEmployees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobEmployee = await _context.JobEmployees.FindAsync(id);
            if (jobEmployee == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", jobEmployee.EmployeeId);
            ViewData["JobCardId"] = new SelectList(_context.Jobs, "JobCardId", "JobCardId", jobEmployee.JobCardId);
            return View(jobEmployee);
        }

        // POST: JobEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobEmployeeId,JobCardId,EmployeeId")] JobEmployee jobEmployee)
        {
            if (id != jobEmployee.JobEmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobEmployee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobEmployeeExists(jobEmployee.JobEmployeeId))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", jobEmployee.EmployeeId);
            ViewData["JobCardId"] = new SelectList(_context.Jobs, "JobCardId", "JobCardId", jobEmployee.JobCardId);
            return View(jobEmployee);
        }

        // GET: JobEmployees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobEmployee = await _context.JobEmployees
                .Include(j => j.Employee)
                .Include(j => j.JobCard)
                .FirstOrDefaultAsync(m => m.JobEmployeeId == id);
            if (jobEmployee == null)
            {
                return NotFound();
            }

            return View(jobEmployee);
        }

        // POST: JobEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobEmployee = await _context.JobEmployees.FindAsync(id);
            _context.JobEmployees.Remove(jobEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobEmployeeExists(int id)
        {
            return _context.JobEmployees.Any(e => e.JobEmployeeId == id);
        }
    }
}
