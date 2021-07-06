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
    public class JobMaterialsController : Controller
    {
        private readonly Domingo_Roof_WorksContext _context;

        public JobMaterialsController(Domingo_Roof_WorksContext context)
        {
            _context = context;
        }

        // GET: JobMaterials
        public async Task<IActionResult> Index()
        {
            var domingo_Roof_WorksContext = _context.JobMaterials.Include(j => j.JobCard).Include(j => j.Material);
            return View(await domingo_Roof_WorksContext.ToListAsync());
        }

        // GET: JobMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobMaterial = await _context.JobMaterials
                .Include(j => j.JobCard)
                .Include(j => j.Material)
                .FirstOrDefaultAsync(m => m.JobMaterialId == id);
            if (jobMaterial == null)
            {
                return NotFound();
            }

            return View(jobMaterial);
        }

        // GET: JobMaterials/Create
        public IActionResult Create()
        {
            ViewData["JobCardId"] = new SelectList(_context.Jobs, "JobCardId", "JobCardId");
            ViewData["MaterialId"] = new SelectList(_context.Materials, "MaterialId", "Description");
            return View();
        }

        // POST: JobMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobMaterialId,JobCardId,MaterialId,Quantity")] JobMaterial jobMaterial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobCardId"] = new SelectList(_context.Jobs, "JobCardId", "JobCardId", jobMaterial.JobCardId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "MaterialId", "Description", jobMaterial.MaterialId);
            return View(jobMaterial);
        }

        // GET: JobMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobMaterial = await _context.JobMaterials.FindAsync(id);
            if (jobMaterial == null)
            {
                return NotFound();
            }
            ViewData["JobCardId"] = new SelectList(_context.Jobs, "JobCardId", "JobCardId", jobMaterial.JobCardId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "MaterialId", "Description", jobMaterial.MaterialId);
            return View(jobMaterial);
        }

        // POST: JobMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobMaterialId,JobCardId,MaterialId,Quantity")] JobMaterial jobMaterial)
        {
            if (id != jobMaterial.JobMaterialId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobMaterialExists(jobMaterial.JobMaterialId))
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
            ViewData["JobCardId"] = new SelectList(_context.Jobs, "JobCardId", "JobCardId", jobMaterial.JobCardId);
            ViewData["MaterialId"] = new SelectList(_context.Materials, "MaterialId", "Description", jobMaterial.MaterialId);
            return View(jobMaterial);
        }

        // GET: JobMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobMaterial = await _context.JobMaterials
                .Include(j => j.JobCard)
                .Include(j => j.Material)
                .FirstOrDefaultAsync(m => m.JobMaterialId == id);
            if (jobMaterial == null)
            {
                return NotFound();
            }

            return View(jobMaterial);
        }

        // POST: JobMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobMaterial = await _context.JobMaterials.FindAsync(id);
            _context.JobMaterials.Remove(jobMaterial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobMaterialExists(int id)
        {
            return _context.JobMaterials.Any(e => e.JobMaterialId == id);
        }
    }
}
