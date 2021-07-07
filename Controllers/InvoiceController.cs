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
            // generated a temporary list of model types that maps to lists 
            List<Employee> employees = db.Employees.ToList();
            List<Customer> customers = db.Customers.ToList();
            List<Job> jobs = db.Jobs.ToList();
            List<Material> materials = db.Materials.ToList();
            List<JobMaterial> job_Materials = db.JobMaterials.ToList();
            List<JobEmployee> job_Employees = db.JobEmployees.ToList();
            List<JobType> jobTypes = db.JobTypes.ToList();

            // saves the sql query to Jobcard
            //Join tables and select the view to be displayed back to IEnumerable<Invoice> JobCard
            IEnumerable<Invoice> JobCard = (from j in jobs
                                            join c in customers on j.CustomerId equals c.CustomerId into T1
                                            from c in T1.ToList()
                                            join jt in jobTypes on j.JobTypeId equals jt.JobTypeId into T4
                                            from jt in T4.ToList()
                                            select new Invoice
                                            {
                                                // Items to be displayed
                                                Jobs = j,
                                                Customer = c,
                                                JobType = jt
                                            });
            // return IEnumerable<Invoice> JobCard of the view to be rendered
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
                // generated another temporary instance of models lists for the details view
                List<Employee> employees = db.Employees.ToList();
                List<Customer> customers = db.Customers.ToList();
                List<Job> jobs = db.Jobs.ToList();
                List<Material> materials = db.Materials.ToList();
                List<JobMaterial> job_Materials = db.JobMaterials.ToList();
                List<JobEmployee> job_Employees = db.JobEmployees.ToList();
                List<JobType> jobTypes = db.JobTypes.ToList();

                // using linq and sql 
                // join all tables with primamry keys
                List<Invoice> JobCard = (from j in jobs
                                         where j.JobCardId.Equals(id)
                                         join c in customers on j.CustomerId equals c.CustomerId into T1
                                         from c in T1.ToList()
                                         join je in job_Employees on j.JobCardId equals je.JobCardId into tbl2
                                         from je in tbl2.ToList()
                                         join e in employees on je.EmployeeId equals e.EmployeeId into tbl3
                                         from e in tbl3.ToList()
                                         join jt in jobTypes on j.JobTypeId equals jt.JobTypeId into tbl4
                                         from jt in tbl4.ToList()
                                         join jm in job_Materials on j.JobCardId equals jm.JobCardId into tbl5
                                         from jm in tbl5.ToList()
                                         join m in materials on jm.MaterialId equals m.MaterialId into tbl6
                                         from m in tbl6.ToList()
                                         select new Invoice
                                         {

                                             // the values being assigned to the instance to be rendered as a display when
                                             // convert all back to a list
                                             Jobs = j,
                                             Customer = c,
                                             Material = m,
                                             Job_Material = jm,
                                             JobType = jt,
                                             Job_Employee = je,
                                             Employee = e

                                         }).ToList();

                // Created the two lists to hold the duplicated values of un normalised data
                List<string> Employees = new List<string>();
                List<string> Materials = new List<string>();

                // loop to iterate through the Invoice in jobcard and save all duplicates 
                foreach (Invoice item in JobCard)
                {

                    // check if the job card selected is the same as the id
                    if (item.Jobs.JobCardId == id)
                    {
                        Materials.Add(" " + item.Job_Material.Quantity + " x " + item.Material.Description);

                        Employees.Add(" " + item.Employee.EmployeeId + " " + item.Employee.Name + " " + item.Employee.Surname);
                    }
                }

                // using a for each loop to access the data in the list 
                foreach(Invoice item in JobCard)
                {
                    if (item.Jobs.JobCardId == id)
                    {

                        // save all items to be displayed to the view bags
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
                // two lists to hold the duplicated fields
                List<string> dplEmployees = new List<string>();
                List<string> dplMaterials = new List<string>();

                // save everything except the duplciated values
                for (int i = 0; i < Employees.Count; i++)
                {
                    if (!dplEmployees.Contains(Employees[i]))
                    {
                        dplEmployees.Add(Employees[i]);
                    }
                }

                // save everything except the duplciated values
                for (int i = 0; i < Materials.Count; i++)
                {
                    if (!dplMaterials.Contains(Materials[i]))
                    {
                        dplMaterials.Add(Materials[i]);
                    }
                }

                // initialise the view bas to the lists
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
