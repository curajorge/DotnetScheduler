using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.API.ViewModels
{
    public class TypesViewModel
    {
        public List<string> WorkTypes { get; set; }
        public string[] JobTypes { get; set; }
        public string[] LeadTypes { get; set; }
    }
}
