﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TechJobs6Persistent.Data;
using TechJobs6Persistent.Models;
using TechJobs6Persistent.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TechJobs6Persistent.Controllers
{
    public class JobController : Controller
    {
        private JobDbContext context;

        public JobController(JobDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).ToList();

            return View(jobs);
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<Employer> employers = context.Employers.ToList();

            AddJobViewModel AddJobViewModel = new AddJobViewModel(employers);

            return View(AddJobViewModel);
            
        }

        [HttpPost]
        public IActionResult Add(AddJobViewModel addJobViewModel)
        {
           if(ModelState.IsValid)
            {
                Employer theEmployer = context.Employers.Find(addJobViewModel.EmployerId);

                Job theJob = new Job
                {
                    Name = addJobViewModel.Name,
                    Employer = theEmployer,

                };

                context.Jobs.Add(theJob);
                context.SaveChanges();

                return Redirect("/Job");

            }
            return View(addJobViewModel);
        }

        public IActionResult Delete()
        {
            ViewBag.jobs = context.Jobs.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] jobIds)
        {
            foreach (int jobId in jobIds)
            {
                Job theJob = context.Jobs.Find(jobId);
                context.Jobs.Remove(theJob);
            }

            context.SaveChanges();

            return Redirect("/Job");
        }

        public IActionResult Detail(int id)
        {
            Job theJob = context.Jobs.Include(j => j.Employer).Include(j => j.Skills).Single(j => j.Id == id);

            JobDetailViewModel jobDetailViewModel = new JobDetailViewModel(theJob);

            return View(jobDetailViewModel);

        }

        public IActionResult Search()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Search(string searchTerm, string? searchType)
        {
            List<Job> jobs = context.Jobs.Include(j => j.Employer).Include(j => j.Skills).ToList();
            ViewBag.jobs = new List<Job>();
            if (searchType == "Employers")
            {
                foreach (Job j in jobs)
                {
                    if (j.Employer.Name == searchTerm)
                    {
                        ViewBag.jobs.Add(j);
                    }
                }
            }
            else if (searchType == "Location")
            {
                foreach (Job j in jobs)
                {
                    if (j.Employer.Location == searchTerm)
                    {
                        ViewBag.jobs.Add(j);
                    }
                }
            }
            else if (searchType == "Skills")
            {
                foreach (Job j in jobs)
                {
                    foreach (Skill skill in j.Skills)
                    {
                        if (skill.SkillName == searchTerm)
                        {
                            ViewBag.jobs.Add(j);
                        }
                    }
                }
            }
            return View();
        }
    }
}

