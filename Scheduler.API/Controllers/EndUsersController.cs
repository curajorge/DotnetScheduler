using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Data.Abstract;
using Scheduler.Model;
using Scheduler.API.ViewModels;
using AutoMapper;
using Scheduler.API.Core;



namespace Scheduler.API.Controllers
{
    [Route("api/[controller]")]
    public class EndUsersController : Controller
    {
        private IEndUserRepository _endUserRepository;
        private IJobAssigneeRepository _jobAssigneeRepository;
        

        int page = 1;
        int pageSize = 10;
        public EndUsersController(IEndUserRepository endUserRepository, IJobAssigneeRepository jobAssigneeRepository)
        {
            _endUserRepository = endUserRepository;
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
            var totalEndUsers = _endUserRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalEndUsers / pageSize);

            IEnumerable<EndUser> _endUsers = _endUserRepository
                //GetAll()
                .AllIncluding(u => u.JobAssignees)
                .OrderBy(u => u.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            IEnumerable<EndUserViewModel> _endUsersVM = Mapper.Map<IEnumerable<EndUser>, IEnumerable<EndUserViewModel>>(_endUsers);

            Response.AddPagination(page, pageSize, totalEndUsers, totalPages);

            return new OkObjectResult(_endUsersVM);
        }

        [HttpGet("{id}", Name = "GetEndUser")]
        public IActionResult Get(int id)
        {
            EndUser _endUser = _endUserRepository.GetSingle(u => u.Id == id );

            if (_endUser != null)
            {
                EndUserViewModel _endUserVM = Mapper.Map<EndUser, EndUserViewModel>(_endUser);
                return new OkObjectResult(_endUserVM);
            }
            else
            {
                return NotFound();
            }
        }
        
       
        [HttpPost]
        public IActionResult Create([FromBody]EndUserViewModel endUser)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EndUser _newEndUser = new EndUser {
                FirstName = endUser.FirstName,
                LastName = endUser.LastName,
                Email = endUser.Email,
                CreatedDate = DateTime.Now.Date,
                Status = JobStatusType.Active,
                                
            };

            _endUserRepository.Add(_newEndUser);
            _endUserRepository.Commit();

            endUser = Mapper.Map<EndUser, EndUserViewModel>(_newEndUser);

            CreatedAtRouteResult result = CreatedAtRoute("GetEndUser", new { controller = "EndUsers", id = endUser.Id }, endUser);
            return result;
        }
        
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]EndUserViewModel endUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EndUser _endUserDb = _endUserRepository.GetSingle(id);

            if (_endUserDb == null)
            {
                return NotFound();
            }
            else
            {

                _endUserDb.FirstName = endUser.FirstName;
                _endUserDb.LastName = endUser.LastName;
                _endUserDb.Email = endUser.Email;                              
                _endUserRepository.Commit();
            }

            endUser = Mapper.Map<EndUser, EndUserViewModel>(_endUserDb);

            return new NoContentResult();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            EndUser _endUserDb = _endUserRepository.GetSingle(id);

            if (_endUserDb == null)
            {
                return new NotFoundResult();
            }
            else
            {
                IEnumerable<JobAssignee> _jobAssignees = _jobAssigneeRepository.FindBy(a => a.EndUserId == id);                

                foreach (var jobAssignee in _jobAssignees)
                {
                    _jobAssigneeRepository.Delete(jobAssignee);
                }

                
                _endUserRepository.Delete(_endUserDb);

                _endUserRepository.Commit();

                return new NoContentResult();
            }
        }

        
    }

}
