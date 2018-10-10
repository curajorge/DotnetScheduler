using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Model
{
    public class Address : IEntityBase
    {
        public int Id { get; set; }
        public AddressType AddressType { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
