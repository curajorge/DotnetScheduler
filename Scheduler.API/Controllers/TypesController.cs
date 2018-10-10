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
    public class TypesController : Controller
    {
        
        
        public IActionResult Get()
        {
            TypesViewModel types = new TypesViewModel();
            List<string> workTypes = new List<string>();

            foreach (WorkType val in Enum.GetValues(typeof(WorkType))) {
                workTypes.Add(val.ToString());
            }
            types.WorkTypes = workTypes;




                return new OkObjectResult(types);
        }

      
        

    }

}
