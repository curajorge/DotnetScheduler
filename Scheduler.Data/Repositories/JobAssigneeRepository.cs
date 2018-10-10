using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scheduler.Model;
using Scheduler.Data;
using Scheduler.Data.Repositories;
using Scheduler.Data.Abstract;

namespace Scheduler.Data.Repositories
{
    public class JobAssigneeRepository : EntityBaseRepository<JobAssignee>, IJobAssigneeRepository
    {
        public JobAssigneeRepository(SchedulerContext context)
            : base(context)
        { }
    }
}
