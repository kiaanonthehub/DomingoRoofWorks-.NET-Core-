﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomingoRoofWorks.Models;

namespace DomingoRoofWorks.Controllers
{
    public class JobTypesController : Controller
    {
        private readonly Domingo_Roof_WorksContext _context;

        public JobTypesController(Domingo_Roof_WorksContext context)
        {
            _context = context;
        }

        // GET: JobTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobTypes.ToListAsync());
        }

        // GET: JobTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobType = await _context.JobTypes
                .FirstOrDefaultAsync(m => m.JobTypeId == id);
            if (jobType == null)
            {
                return NotFound();
            }

            return View(jobType);
        }

        // GET: JobTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobTypeId,JobType1,Rate")] JobType jobType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobType);
        }

        // GET: JobTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobType = await _context.JobTypes.FindAsync(id);
            if (jobType == null)
            {
                return NotFound();
            }
            return View(jobType);
        }

        // POST: JobTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobTypeId,JobType1,Rate")] JobType jobType)
        {
            if (id != jobType.JobTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobTypeExists(jobType.JobTypeId))
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
            return View(jobType);
        }

        // GET: JobTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobType = await _context.JobTypes
                .FirstOrDefaultAsync(m => m.JobTypeId == id);
            if (jobType == null)
            {
                return NotFound();
            }

            return View(jobType);
        }

        // POST: JobTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobType = await _context.JobTypes.FindAsync(id);
            _context.JobTypes.Remove(jobType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobTypeExists(int id)
        {
            return _context.JobTypes.Any(e => e.JobTypeId == id);
        }
    }
}
