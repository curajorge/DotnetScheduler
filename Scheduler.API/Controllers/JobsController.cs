using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Data.Abstract;
using Scheduler.Model;
using Scheduler.API.ViewModels;
using AutoMapper;
using Scheduler.API.Core;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Scheduler.API.Controllers
{
    [Route("api/[controller]")]
    public class JobsController : Controller
    {
        private IEndUserRepository _endUserRepository;
        private IJobRepository _jobRepository;
        private IWorkDescriptionRepository _workDescriptionRepository;
        private IJobAssigneeRepository _jobAssigneeRepository;

        int page = 1;
        int pageSize = 10;
        public JobsController(IEndUserRepository endUserRepository,
                                IJobRepository jobRepository,
                                IWorkDescriptionRepository workDescriptionRepository,
                                IJobAssigneeRepository jobAssigneeRepository)
        {
            _endUserRepository = endUserRepository;
            _jobRepository = jobRepository;
            _workDescriptionRepository = workDescriptionRepository;
            _jobAssigneeRepository = jobAssigneeRepository;
        }

        public IActionResult Get()
        {
            var pagination = Request.Headers["Pagination"];

            if (!string.IsNullOrEmpty(pagination))
            {
                string[] vals = pagination.ToString().Split(',');
                int.TryParse(vals[0], out page);
                int.TryParse(vals[1], out pageSize);
            }

            int currentPage = page;
            int currentPageSize = pageSize;
            var totalJobs = _jobRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalJobs / pageSize);


            //IEnumerable<JobAssignee> _jobAssignee = _jobAssigneeRepository.AllIncluding(s => s.Job, s => s.EndUser);


            IEnumerable<Job> _jobs = _jobRepository
                .AllIncluding(j => j.JobAssignees, j => j.WorkDescriptions, j => j.Address, j => j.Customer)                
                .OrderBy(u => u.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            IEnumerable<JobViewModel> _jobsVM = Mapper.Map<IEnumerable<Job>, IEnumerable<JobViewModel>>(_jobs);

            Response.AddPagination(page, pageSize, totalJobs, totalPages);

            return new OkObjectResult(_jobsVM);
        }
        
        [HttpGet("{id}", Name = "GetJob")]
        public IActionResult Get(int id)
        {
            Job _job = _jobRepository.GetSingle(u => u.Id == id,
                                            s => s.WorkDescriptions,
                                        s => s.JobAssignees,
                                   s => s.Address, j => j.Customer);
           
            if (_job != null)
            {
                JobViewModel _jobVM = Mapper.Map<Job, JobViewModel>(_job);
                return new OkObjectResult(_jobVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{jobId}")]
        public IActionResult PutJob(int jobId, [FromBody]JobViewModel job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Job _jobDb = _jobRepository.GetSingle(jobId);

            if (_jobDb == null)
            {
                return NotFound();
            }
            else
            {
                
                _jobDb.JobType = (JobType)Enum.Parse(typeof(JobType), job.JobType);
                _jobDb.Status = (JobStatusType)Enum.Parse(typeof(JobStatusType), job.Status);
                _jobDb.Date = job.Date;
                _jobDb.AddressId = job.AddressId;

                _jobRepository.Commit();
            }

            job = Mapper.Map<Job, JobViewModel>(_jobDb);

            return new NoContentResult();
        }

        [HttpDelete("jobs/{Id}")]
        public IActionResult DeleteJob(int Id)
        {

            Job _jobDb = _jobRepository.GetSingle(Id);

            if (_jobDb == null)
            {
                return new NotFoundResult();
            }
            else
            {
                IEnumerable<JobAssignee> _jobAssignees = _jobAssigneeRepository.FindBy(a => a.JobId == Id);
                IEnumerable<WorkDescription> _workDescriptions = _workDescriptionRepository.FindBy(s => s.JobId == Id);

                foreach (var jobAssignee in _jobAssignees)
                {
                    _jobAssigneeRepository.Delete(jobAssignee);
                }

                foreach (var workDescription in _workDescriptions)
                {
                    _workDescriptionRepository.Delete(workDescription);
                }

                _jobRepository.Delete(_jobDb);

                _jobRepository.Commit();

                return new NoContentResult();
            }
        }

        //////////////////////////
        /////WorkDescription//////
        /////////////////////////

        [HttpGet("{id}/workdescriptions", Name = "GetJobWorkDescription")]
        public IActionResult GetWorkDescriptions(int id)
        {
            IEnumerable<WorkDescription> _jobWorkDescription = _workDescriptionRepository.FindBy(s => s.JobId == id);

            if (_jobWorkDescription != null)
            {
                IEnumerable<WorkDescription> _jobWorkDescriptionVM = Mapper.Map<IEnumerable<WorkDescription>, IEnumerable<WorkDescription>>(_jobWorkDescription);
                return new OkObjectResult(_jobWorkDescriptionVM);
            }
            else
            {
                return NotFound();
            }
        }

        
        [HttpPost("{id}/workdescriptions")]
        public IActionResult CreateWorkDescription(int id, [FromBody]WorkDescriptionViewModel workDescription)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Job  _jobDb = _jobRepository.GetSingle(id);
            WorkDescription _newWorkDescription;
            if (_jobDb == null)
            {
                return NotFound();
            }
            else
            {
                _newWorkDescription = new WorkDescription
                {
                    WorkType = (WorkType)Enum.Parse(typeof(WorkType), workDescription.WorkType),
                    Revenue = workDescription.Revenue,
                    JobId = id
                };
            }
            _workDescriptionRepository.Add(_newWorkDescription);
            _workDescriptionRepository.Commit();

            workDescription = Mapper.Map<WorkDescription, WorkDescriptionViewModel>(_newWorkDescription);

            CreatedAtRouteResult result = CreatedAtRoute("GetJobWorkDescription", new { controller = "Jobs", id });
            return result;
        }

        [HttpPut("{id}/workdescriptions/{workDescriptionId}")]
        public IActionResult PutAddress(int id, int workDescriptionId, [FromBody]WorkDescriptionViewModel workDescription)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            WorkDescription _workDescriptionDb = _workDescriptionRepository.GetSingle(workDescriptionId);

            if (_workDescriptionDb == null)
            {
                return NotFound();
            }
            else
            {
                _workDescriptionDb.WorkType = (WorkType)Enum.Parse(typeof(WorkType), workDescription.WorkType);
                _workDescriptionDb.Revenue = workDescription.Revenue;
                _workDescriptionRepository.Commit();
                
            }

            workDescription = Mapper.Map<WorkDescription, WorkDescriptionViewModel>(_workDescriptionDb);

            return new NoContentResult();
        }

        [HttpDelete("{id}/workdescriptions/{workDescriptionId}")]
        public IActionResult DeleteWorkDescription(int workDescriptionId)
        {
            WorkDescription _workDescriptionDb = _workDescriptionRepository.GetSingle(workDescriptionId);

            if (_workDescriptionDb == null)
            {
                return new NotFoundResult();
            }
            else
            {

                _workDescriptionRepository.Delete(_workDescriptionDb);

                _workDescriptionRepository.Commit();

                return new NoContentResult();
            }
        }

        /////////////////////////
        ////////JobAssignee/////
        ////////////////////////

        [HttpGet("{id}/jobassignees", Name = "GetJobAssignees")]
        public IActionResult GetJobAssignees(int id)
        {
            //IEnumerable<JobAssignee> _jobAssignee = _jobAssigneeRepository.FindBy(s => s.JobId == id);

            IEnumerable<JobAssignee> _jobAssignee = _jobAssigneeRepository.AllIncluding(s => s.Job, s => s.EndUser).Where(s => s.JobId==id);
            

            

            if (_jobAssignee != null)
            {
                IEnumerable<JobAssigneeViewModel> _jobAssigneeVM = Mapper.Map<IEnumerable<JobAssignee>, IEnumerable<JobAssigneeViewModel>>(_jobAssignee);
                return new OkObjectResult(_jobAssigneeVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{jobId}/jobassignees/")]
        public IActionResult CreateJobAssignee(int jobId,[FromBody]JobAssigneeViewModel jobAssignee)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Job _jobDb = _jobRepository.GetSingle(jobId);

            JobAssignee _newJobAssignee;
            if (_jobDb == null)
            {
                return NotFound();
            }
            else
            {
                _newJobAssignee = new JobAssignee
                {
                    
                    EndUserId = jobAssignee.EndUserId,
                    JobId = jobAssignee.JobId
                    
                };
            }

            
            _jobAssigneeRepository.Add(_newJobAssignee);
            _jobAssigneeRepository.Commit();
            
            //jobAssignee = Mapper.Map<JobAssignee, JobAssigneeViewModel>(_newJobAssignee);

            //CreatedAtRouteResult result = CreatedAtRoute("GetJobWorkDescription", new { controller = "Jobs", id });
            //return result;
            return new NoContentResult();
        }

        [HttpDelete("{id}/jobassignees/{jobAssigneeId}")]
        public IActionResult DeleteJobAssignee(int jobAssigneeId)
        {
            JobAssignee _jobAssigneeDb = _jobAssigneeRepository.GetSingle(jobAssigneeId);

            if (_jobAssigneeDb == null)
            {
                return new NotFoundResult();
            }
            else
            {

                _jobAssigneeRepository.Delete(_jobAssigneeDb);

                _jobAssigneeRepository.Commit();

                return new NoContentResult();
            }
        }

    }

}
