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
    public class CustomersController : Controller
    {
        private ICustomerRepository _customerRepository;
        private IPhoneNumberRepository _phoneNumberRepository;
        private IAddressRepository _addressRepository;
        private IEndUserRepository _endUserRepository;
        private IJobRepository _jobRepository;
        private IWorkDescriptionRepository _workDescriptionRepository;
        private IJobAssigneeRepository _jobAssigneeRepository;

        int page = 1;
        int pageSize = 10;
        public CustomersController(ICustomerRepository customerRepository,
                                IPhoneNumberRepository phoneNumberRepository,
                                IAddressRepository addressRepository,
                                IEndUserRepository endUserRepository,
                                IJobRepository jobRepository,
                                IWorkDescriptionRepository workDescriptionRepository,
                                IJobAssigneeRepository jobAssigneeRepository)
        {
            _customerRepository = customerRepository;
            _phoneNumberRepository = phoneNumberRepository;
            _addressRepository = addressRepository;
            _endUserRepository = endUserRepository;
            _jobRepository = jobRepository;
            _workDescriptionRepository = workDescriptionRepository;
            _jobAssigneeRepository = jobAssigneeRepository;
        }

        
        /////////////
        //Customers//
        /////////////

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
            var totalCustomers = _customerRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalCustomers / pageSize);

            IEnumerable<Customer> _customers = _customerRepository
                .AllIncluding(u => u.Addresses, u => u.PhoneNumbers, u => u.LeadType)
                .OrderBy(u => u.FirstName)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            IEnumerable<CustomerViewModel> _customerVM = Mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(_customers);



            Response.AddPagination(page, pageSize, totalCustomers, totalPages);

            return new OkObjectResult(_customerVM);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            Customer _customer = _customerRepository.GetSingle(c => c.Id == id, p => p.PhoneNumbers, a => a.Addresses, u => u.LeadType);

            if (_customer != null)
            {
                CustomerViewModel _customerVM = Mapper.Map<Customer, CustomerViewModel>(_customer);
                return new OkObjectResult(_customerVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Create([FromBody]CustomerViewModel customer)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer _newCustomer = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                CompanyName = customer.CompanyName,                
                LeadTypeId = customer.LeadTypeId
            };

            _customerRepository.Add(_newCustomer);
            _customerRepository.Commit();

            customer = Mapper.Map<Customer, CustomerViewModel>(_newCustomer);

            CreatedAtRouteResult result = CreatedAtRoute("GetCustomer", new { controller = "Customer", id = customer.Id }, customer);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer _customerDb = _customerRepository.GetSingle(id);

            if (_customerDb == null)
            {
                return NotFound();
            }
            else
            {
                _customerDb.FirstName = customer.FirstName;
                _customerDb.LastName = customer.LastName;
                _customerDb.Email = customer.Email;
                _customerDb.CompanyName = customer.CompanyName;
                //_customerDb.Lead = (LeadType)Enum.Parse(typeof(LeadType), customer.Lead);
                _customerDb.LeadTypeId = customer.LeadTypeId;
                _customerRepository.Commit();
            }

            customer = Mapper.Map<Customer, CustomerViewModel>(_customerDb);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Customer _customerDb = _customerRepository.GetSingle(id);

            if (_customerDb == null)
            {
                return new NotFoundResult();
            }
            else
            {
                IEnumerable<PhoneNumber> _phoneNumbers = _phoneNumberRepository.FindBy(a => a.CustomerId == id);
                IEnumerable<Address> _addresses = _addressRepository.FindBy(s => s.CustomerId == id);

                foreach (var phoneNumber in _phoneNumbers)
                {
                    _phoneNumberRepository.Delete(phoneNumber);
                }

                foreach (var address in _addresses)
                {
                    _addressRepository.Delete(address);
                }

                _customerRepository.Delete(_customerDb);

                _customerRepository.Commit();

                return new NoContentResult();
            }
        }


        /////////////
        //Addresses//
        /////////////

        [HttpGet("{id}/Addresses", Name = "GetCustomerAddresses")]
        public IActionResult GetSchedules(int id)
        {
            IEnumerable<Address> _customerAddresses = _addressRepository.FindBy(s => s.CustomerId == id);

            if (_customerAddresses != null)
            {
                IEnumerable<AddressViewModel> _customerAddressesVM = Mapper.Map<IEnumerable<Address>, IEnumerable<AddressViewModel>>(_customerAddresses);
                return new OkObjectResult(_customerAddressesVM);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("{id}/addresses")]
        public IActionResult CreateAddress(int id, [FromBody]AddressViewModel address, [FromBody]CustomerViewModel customer)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer _customerDb = _customerRepository.GetSingle(id);
            Address _newAddress;
            if (_customerDb == null)
            {
                return NotFound();
            }
            else
            {
                _newAddress = new Address
                {
                    Address1 = address.Address1,
                    Address2 = address.Address2,
                    City = address.City,
                    ZipCode = address.ZipCode,
                    State = address.State,
                    CustomerId = id
                };
            }
            _addressRepository.Add(_newAddress);
            _addressRepository.Commit();

            address = Mapper.Map<Address, AddressViewModel>(_newAddress);

            CreatedAtRouteResult result = CreatedAtRoute("GetAddress", new { controller = "Address", id = address.Id }, address);
            return result;
        }
        
        [HttpPut("{id}/addresses/{addressId}")]
        public IActionResult PutAddress(int id, int addressId, [FromBody]AddressViewModel address)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            Address _addressDb = _addressRepository.GetSingle(addressId);

            if (_addressDb == null)
            {
                return NotFound();
            }
            else
            {
                _addressDb.Address1 = address.Address1;
                _addressDb.Address2 = address.Address2;
                _addressDb.City = address.City;
                _addressDb.State = address.State;
                _addressDb.ZipCode = address.ZipCode;
                _customerRepository.Commit();
            }

            address = Mapper.Map<Address, AddressViewModel>(_addressDb);

            return new NoContentResult();
        }


        ////////////////
        //PhoneNumbers//
        ////////////////

        [HttpGet("{id}/PhoneNumbers", Name = "GetCustomerPhoneNumbers")]
        public IActionResult GetPhoneNumbers(int id)
        {
            IEnumerable<PhoneNumber> _customerPhoneNumbers = _phoneNumberRepository.FindBy(s => s.CustomerId == id);

            if (_customerPhoneNumbers != null)
            {
                IEnumerable<PhoneNumberViewModel> _customerPhoneNumbersVM = Mapper.Map<IEnumerable<PhoneNumber>, IEnumerable<PhoneNumberViewModel>>(_customerPhoneNumbers);
                return new OkObjectResult(_customerPhoneNumbersVM);
            }
            else
            {
                return NotFound();
            }
        }

       

        [HttpPost("{id}/phonenumbers")]
        public IActionResult CreatePhoneNumber(int id, [FromBody]PhoneNumberViewModel phoneNumber)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer _customerDb = _customerRepository.GetSingle(id);
            PhoneNumber _newPhoneNumber;
            if (_customerDb == null)
            {
                return NotFound();
            }
            else
            {
                _newPhoneNumber = new PhoneNumber
                {
                    Number = phoneNumber.Number,
                    CustomerId = id
                };
            }
            _phoneNumberRepository.Add(_newPhoneNumber);
            _phoneNumberRepository.Commit();

            phoneNumber = Mapper.Map<PhoneNumber, PhoneNumberViewModel>(_newPhoneNumber);

            CreatedAtRouteResult result = CreatedAtRoute("GetCustomerPhoneNumbers", new { controller = "Customers", id}, phoneNumber);
            return result;
        }

        [HttpPut("{id}/phonenumbers/{phoneNumberId}")]
        public IActionResult PutAddress(int id, int phoneNumberId, [FromBody]PhoneNumberViewModel phoneNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            PhoneNumber _phoneNumberDb = _phoneNumberRepository.GetSingle(phoneNumberId);

            if (_phoneNumberDb == null)
            {
                return NotFound();
            }
            else
            {
                _phoneNumberDb.Number = phoneNumber.Number;
                _phoneNumberDb.CustomerId = id;
                _phoneNumberRepository.Commit();                
            }

            phoneNumber = Mapper.Map<PhoneNumber, PhoneNumberViewModel>(_phoneNumberDb);

            return new NoContentResult();
        }

        

        [HttpDelete("{id}/phonenumbers/{phoneNumberId}")]
        public IActionResult DeletePhoneNumber(int phoneNumberId)
        {
            PhoneNumber _phoneNumberDb = _phoneNumberRepository.GetSingle(phoneNumberId);

            if (_phoneNumberDb == null)
            {
                return new NotFoundResult();
            }
            else
            {

                _phoneNumberRepository.Delete(_phoneNumberDb);

                _customerRepository.Commit();

                return new NoContentResult();
            }
        }

        ////////////////
        //////Jobs//////
        ////////////////

        [HttpGet("{id}/Jobs", Name = "GetCustomerJobs")]
        public IActionResult GetJobs(int id)
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

            IEnumerable<Job> _jobs = _jobRepository
                .AllIncluding(j => j.JobAssignees, j => j.WorkDescriptions, j => j.Address)
                .Where(j => j.CustomerId == id)
                .OrderBy(u => u.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            IEnumerable<JobViewModel> _jobsVM = Mapper.Map<IEnumerable<Job>, IEnumerable<JobViewModel>>(_jobs);

            Response.AddPagination(page, pageSize, totalJobs, totalPages);

            return new OkObjectResult(_jobsVM);
        }

        [HttpPost("{id}/jobs")]
        public IActionResult CreateJob(int id, [FromBody]JobViewModel job)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer _customerDb = _customerRepository.GetSingle(id);
            Job _newJob;
            if (_customerDb == null)
            {
                return NotFound();
            }
            else
            {
                _newJob = new Job
                {
                    JobType = (JobType)Enum.Parse(typeof(JobType), job.JobType),
                    Date = DateTime.Now.Date,
                    Status = JobStatusType.Active,
                    CustomerId = id
                };
            }
            _jobRepository.Add(_newJob);
            _jobRepository.Commit();

            job = Mapper.Map<Job, JobViewModel>(_newJob);

            //check logic - return jobs created from customer To-Do 
            CreatedAtRouteResult result = CreatedAtRoute("GetCustomerJobs", new { controller = "Customers", id }, job);
            return result;
        }      

        [HttpPut("{id}/jobs/{jobId}")]
        public IActionResult PutJob(int id, int jobId, [FromBody]JobViewModel job)
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
                //Here
                _jobDb.JobType = (JobType)Enum.Parse(typeof(JobType), job.JobType);
                
                _jobRepository.Commit();
            }

            job = Mapper.Map<Job, JobViewModel>(_jobDb);

            return new NoContentResult();
        }

        [HttpDelete("{id}/jobs/{jobId}")]
        public IActionResult DeleteJob(int id, int jobId)
        {
            Job _jobDb = _jobRepository.GetSingle(jobId);

            if (_jobDb == null)
            {
                return new NotFoundResult();
            }
            else
            {
                IEnumerable<JobAssignee> _jobAssignees = _jobAssigneeRepository.FindBy(a => a.JobId == jobId);
                IEnumerable<WorkDescription> _workDescriptions = _workDescriptionRepository.FindBy(s => s.JobId == jobId);

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


    }

}
