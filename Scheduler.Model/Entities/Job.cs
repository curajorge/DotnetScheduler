using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Model
{
    public class Job : IEntityBase
    {
        public Job()
        {
            WorkDescriptions = new List<WorkDescription>();            
            JobAssignees = new List<JobAssignee>();
        }
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public JobType JobType { get; set; }
        public int CustomerId { get; set; }
        public JobStatusType Status { get; set; }
        public Customer Customer { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        public ICollection<WorkDescription> WorkDescriptions { get; set; }
        public ICollection<JobAssignee> JobAssignees  { get; set; }
        
    }
}
