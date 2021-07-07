using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DomingoRoofWorks.Models;
using System.Dynamic;
using DomingoRoofWorks;

namespace DomingoRoofWorks.Controllers
{
    public class JobsController : Controller
    {
        private readonly Domingo_Roof_WorksContext _context;

        public JobsController(Domingo_Roof_WorksContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            dynamic dy = new ExpandoObject();
            dy.jobEmpList = GetJobEmployees();
            dy.jobMatList = GetJobMaterials();
            dy.jobList = GetJobs();

            var domingo_Roof_WorksContext = _context.Jobs.Include(j => j.Customer).Include(j => j.JobType);
            return View(await domingo_Roof_WorksContext.ToListAsync());
        }

        public List<JobEmployee> GetJobEmployees()
        {
            Domingo_Roof_WorksContext db = new Domingo_Roof_WorksContext();
            List<JobEmployee> je = db.JobEmployees.ToList();
            return je;
        }
        public List<JobMaterial> GetJobMaterials() 
        {
            Domingo_Roof_WorksContext db = new Domingo_Roof_WorksContext();
            List<JobMaterial> jm = db.JobMaterials.ToList();
            return jm;
        }

        public List<Job> GetJobs()
        {
            Domingo_Roof_WorksContext db = new Domingo_Roof_WorksContext();
            List<Job> j = db.Jobs.ToList();
            return j;
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .Include(j => j.Customer)
                .Include(j => j.JobType)
                .FirstOrDefaultAsync(m => m.JobCardId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name");
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "JobTypeId", "JobType1");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobCardId,CustomerId,JobTypeId,Days")] Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", job.CustomerId);
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "JobTypeId", "JobType1", job.JobTypeId);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name", job.CustomerId);
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "JobTypeId", "JobType1", job.JobTypeId);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobCardId,CustomerId,JobTypeId,Days")] Job job)
        {
            if (id != job.JobCardId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.JobCardId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Address", job.CustomerId);
            ViewData["JobTypeId"] = new SelectList(_context.JobTypes, "JobTypeId", "JobType1", job.JobTypeId);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs
                .Include(j => j.Customer)
                .Include(j => j.JobType)
                .FirstOrDefaultAsync(m => m.JobCardId == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.JobCardId == id);
        }
    }
}
