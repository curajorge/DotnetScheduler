using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Model
{
    public class Customer : IEntityBase
    {
        public Customer()
        {
            PhoneNumbers = new List<PhoneNumber>();
            Addresses = new List<Address>();
            Jobs = new List<Job>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public LeadType? Lead { get; set; }
        public DateTime DateCreated { get; set; }
        //To-Do
            
        public int? LeadTypeId { get; set; }
        public Lead LeadType { get; set; }
        public string Email { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Job> Jobs { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }


    }
}
