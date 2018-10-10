using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Model
{
    public class PhoneNumber : IEntityBase
    {
        public int Id { get; set; }        
        public string Number { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
