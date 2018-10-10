using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Model
{
    public class JobAssignee : IEntityBase
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public int EndUserId { get; set; }
        public EndUser EndUser { get; set; }
    }
}
