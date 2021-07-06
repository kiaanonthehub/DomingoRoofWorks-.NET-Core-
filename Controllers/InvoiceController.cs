using DomingoRoofWorks.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomingoRoofWorks.Controllers
{
    public class InvoiceController : Controller
    {
        Domingo_Roof_WorksContext db = new Domingo_Roof_WorksContext();

        public IActionResult Index()
        {

            List<Employee> employees = db.Employees.ToList();
            List<Customer> customers = db.Customers.ToList();
            List<Job> jobs = db.Jobs.ToList();
            List<Material> materials = db.Materials.ToList();
            List<JobMaterial> job_Materials = db.JobMaterials.ToList();
            List<JobEmployee> job_Employees = db.JobEmployees.ToList();
            List<JobType> jobTypes = db.JobTypes.ToList();

            IEnumerable<Invoice> JobCard = (from j in jobs
                                            join c in customers on j.CustomerId equals c.CustomerId into T1
                                            from c in T1.ToList()
                                            join jt in jobTypes on j.JobTypeId equals jt.JobTypeId into T4
                                            from jt in T4.ToList()
                                            select new Invoice
                                            {
                                                Jobs = j,
                                                Customer = c,
                                                JobType = jt
                                            });

            return View(JobCard);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (Domingo_Roof_WorksContext db = new Domingo_Roof_WorksContext())
            {

                List<Employee> employees = db.Employees.ToList();
                List<Customer> customers = db.Customers.ToList();
                List<Job> jobs = db.Jobs.ToList();
                List<Material> materials = db.Materials.ToList();
                List<JobMaterial> job_Materials = db.JobMaterials.ToList();
                List<JobEmployee> job_Employees = db.JobEmployees.ToList();
                List<JobType> jobTypes = db.JobTypes.ToList();

                List<Invoice> JobCard = (from j in jobs
                                         where j.JobCardId.Equals(id)
                                         join c in customers on j.CustomerId equals c.CustomerId into T1
                                         from c in T1.ToList()
                                         join je in job_Employees on j.JobCardId equals je.JobCardId into T2
                                         from je in T2.ToList()
                                         join e in employees on je.EmployeeId equals e.EmployeeId into T3
                                         from e in T3.ToList()
                                         join jt in jobTypes on j.JobTypeId equals jt.JobTypeId into T4
                                         from jt in T4.ToList()
                                         join jm in job_Materials on j.JobCardId equals jm.JobCardId into T5
                                         from jm in T5.ToList()
                                         join m in materials on jm.MaterialId equals m.MaterialId into T6
                                         from m in T6.ToList()
                                         select new Invoice
                                         {
                                             Jobs = j,
                                             Customer = c,
                                             Material = m,
                                             Job_Material = jm,
                                             JobType = jt,
                                             Job_Employee = je,
                                             Employee = e

                                         }).ToList();

                List<string> Employees = new List<string>();
                List<string> Materials = new List<string>();

                foreach (Invoice item in JobCard)
                {
                    if (item.Jobs.JobCardId == id)
                    {
                        Materials.Add(" " + item.Job_Material.Quantity + " x " + item.Material.Description);

                        Employees.Add(" " + item.Employee.EmployeeId + " " + item.Employee.Name + " " + item.Employee.Surname);
                    }
                }

                foreach(Invoice item in JobCard)
                {
                    if (item.Jobs.JobCardId == id)
                    {
                        ViewBag.JobCardId = item.Jobs.JobCardId;
                        ViewBag.CustomerName = item.Customer.Name + " " + item.Customer.Surname;
                        ViewBag.Address = item.Customer.Address + ", " + item.Customer.City + "," + item.Customer.PostalCode;
                        ViewBag.JobType = item.JobType.JobType1;
                        ViewBag.Days = item.Jobs.Days;
                        ViewBag.Rate = item.JobType.Rate;
                        ViewBag.TotalExclVat = (item.JobType.Rate * item.Jobs.Days);
                        ViewBag.VAT = Convert.ToDouble(item.JobType.Rate * item.Jobs.Days) * 0.14;
                        ViewBag.TotalInclVat = Convert.ToDouble(item.JobType.Rate * item.Jobs.Days) * 1.14;
                    }
                }

                List<string> dplEmployees = new List<string>();
                List<string> dplMaterials = new List<string>();

                for (int i = 0; i < Employees.Count; i++)
                {
                    if (!dplEmployees.Contains(Employees[i]))
                    {
                        dplEmployees.Add(Employees[i]);
                    }
                }

                for (int i = 0; i < Materials.Count; i++)
                {
                    if (!dplMaterials.Contains(Materials[i]))
                    {
                        dplMaterials.Add(Materials[i]);
                    }
                }

                ViewBag.Employees = dplEmployees;
                ViewBag.Materials = dplMaterials;

                if (JobCard == null)
                {
                    return NotFound();
                }

                return View();
            }
        }
    }
}
