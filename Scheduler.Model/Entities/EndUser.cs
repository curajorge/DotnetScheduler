using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Model
{
    public class EndUser : IEntityBase
    {

        public EndUser()
        {
            JobAssignees = new List<JobAssignee>();  
                      
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
        public JobStatusType Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastLogging { get; set; }
        public ICollection<JobAssignee> JobAssignees { get; set; }
        
    }
}
