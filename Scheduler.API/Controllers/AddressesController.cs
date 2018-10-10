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
    public class AddressesController : Controller
    {
        private IAddressRepository _addressRepository;
        
        int page = 1;
        int pageSize = 10;
        public AddressesController(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
            
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
            var totalAddress = _addressRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalAddress / pageSize);

            IEnumerable<Address> _address = _addressRepository
                .GetAll()
                .OrderBy(u => u.Id)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .ToList();

            IEnumerable<AddressViewModel> _addressVM = Mapper.Map<IEnumerable<Address>, IEnumerable<AddressViewModel>>(_address);

            Response.AddPagination(page, pageSize, totalAddress, totalPages);

            return new OkObjectResult(_addressVM);
        }
        
        [HttpGet("{id}", Name = "GetAddress")]
        public IActionResult Get(int id)
        {
            Address _address = _addressRepository.GetSingle(u => u.Id == id);

            if (_address != null)
            {
                AddressViewModel _addressVM = Mapper.Map<Address, AddressViewModel>(_address);
                return new OkObjectResult(_addressVM);
            }
            else
            {
                return NotFound();
            }
        }
                        

        [HttpDelete("{id}")]
        public IActionResult DeleteAddress(int id)
        {
            Address _addressDb = _addressRepository.GetSingle(id);

            if (_addressDb == null)
            {
                return new NotFoundResult();
            }
            else
            {
                _addressRepository.Delete(_addressDb);

                _addressRepository.Commit();

                return new NoContentResult();
            }
        }
        
    }

}
