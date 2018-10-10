using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Model
{
    public class WorkDescription : IEntityBase
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        public Job Job { get; set; }
        public WorkType WorkType { get; set; }
        public decimal Revenue { get; set; }
    }
}
