using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Data.Abstract;
using Scheduler.Model;
using Scheduler.API.ViewModels;
using AutoMapper;
using Scheduler.API.Core;
using Scheduler.Data;


namespace Scheduler.API.Controllers 
{
    [Route("api/[controller]")]
    public class LeadReportsController : Controller
    {

        private ICustomerRepository _customerRepository;
        private IJobRepository _jobRepository;
        private IWorkDescriptionRepository _workDescriptionRepository;
        private ILeadRepository _leadRepository;

        public LeadReportsController(ICustomerRepository customerRepository,
                                IJobRepository jobRepository,
                                IWorkDescriptionRepository workDescriptionRepository,
                                ILeadRepository leadRepository)
        {
            _customerRepository = customerRepository;
            _jobRepository = jobRepository;
            _workDescriptionRepository = workDescriptionRepository;
            _leadRepository = leadRepository;

        }

        public IActionResult Get()
        {

                       

            List<LeadReportViewModel> leadReports = new List<LeadReportViewModel>();
            IEnumerable<Lead> leadNames = _leadRepository.GetAll();
            
            

            foreach (Lead val in leadNames)
            {
                LeadReportViewModel leadReport = new LeadReportViewModel();

                //total Revenue 
                leadReport.TotalRevenue = _workDescriptionRepository.AllIncluding(c => c.Job.Customer, j => j.Job, c => c.Job.Customer.LeadType)
                    .Where(c => c.Job.Customer.LeadType.Name == val.Name.ToString())
                    .Sum(c => c.Revenue);
                //
                leadReport.Lead = val.Name.ToString();
                //
                leadReport.NetProfit = leadReport.TotalRevenue - val.WeeklyCost;
                //
                leadReport.CallsReceived = _customerRepository.AllIncluding(s => s.LeadType).Where(c => c.LeadType.Name == val.Name).Count();
                //
                leadReport.TotalAppt = _jobRepository.AllIncluding(s => s.Customer.LeadType)
                    .Where(c => c.Customer.LeadType.Name == val.Name).Count();
                //
                try
                {
                    leadReport.BookedPercent = Math.Round((leadReport.TotalAppt / leadReport.CallsReceived) * 100, 2);
                }
                catch (DivideByZeroException)
                { leadReport.BookedPercent = 0; }
                //
                try
                {
                    leadReport.CostCall = val.WeeklyCost / leadReport.CallsReceived;
                        }
                catch (DivideByZeroException) {
                    leadReport.CostCall = 0;
                }
                //
                try
                {
                    leadReport.CostLead = val.WeeklyCost / leadReport.TotalAppt;
                }
                catch (DivideByZeroException)
                {
                    leadReport.CostLead = 0;
                }

                leadReport.LeadCost = val.WeeklyCost;

                leadReports.Add(leadReport);


                
            }
            
            return new OkObjectResult(leadReports);
        }


        [HttpGet("{date}/{todate}", Name = "GetByDate")]
        public IActionResult Get(DateTime date, DateTime todate)
        {


            List<LeadReportViewModel> leadReports = new List<LeadReportViewModel>();
            IEnumerable<Lead> leadNames = _leadRepository.GetAll();



            foreach (Lead val in leadNames)
            {
                LeadReportViewModel leadReport = new LeadReportViewModel();

                decimal LeadPricePerDay = val.WeeklyCost/(date - todate).Days;
                //total Revenue 
                leadReport.TotalRevenue = _workDescriptionRepository.AllIncluding(c => c.Job.Customer, j => j.Job, c => c.Job.Customer.LeadType)
                    .Where(c => c.Job.Customer.LeadType.Name == val.Name.ToString())
                    .Where(c => c.Job.Customer.DateCreated >= date)
                    .Where(c => c.Job.Customer.DateCreated <= todate)
                    .Sum(c => c.Revenue);
                //
                leadReport.Lead = val.Name.ToString();
                //
                leadReport.NetProfit = leadReport.TotalRevenue - LeadPricePerDay;
                //
                leadReport.CallsReceived = _customerRepository.AllIncluding(s => s.LeadType)
                    .Where(c => c.LeadType.Name == val.Name)
                    .Where(c => c.DateCreated >= date)
                    .Where(c => c.DateCreated <= todate)
                    .Count();
                //
                leadReport.TotalAppt = _jobRepository.AllIncluding(s => s.Customer.LeadType)
                    .Where(c => c.Customer.LeadType.Name == val.Name)
                    .Where(c => c.Customer.DateCreated >= date)
                    .Where(c => c.Customer.DateCreated <= date)
                    .Count();
                //
                try
                {
                    leadReport.BookedPercent = Math.Round((leadReport.TotalAppt / leadReport.CallsReceived) * 100, 2);
                }
                catch (DivideByZeroException)
                { leadReport.BookedPercent = 0; }
                //
                try
                {
                    leadReport.CostCall = LeadPricePerDay / leadReport.CallsReceived;
                }
                catch (DivideByZeroException)
                {
                    leadReport.CostCall = 0;
                }
                //
                try
                {
                    leadReport.CostLead = LeadPricePerDay / leadReport.TotalAppt;
                }
                catch (DivideByZeroException)
                {
                    leadReport.CostLead = 0;
                }

                leadReport.LeadCost = LeadPricePerDay;

                leadReports.Add(leadReport);



            }

            return new OkObjectResult(leadReports);



        }


    }

}
