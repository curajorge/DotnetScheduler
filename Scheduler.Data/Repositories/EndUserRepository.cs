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
    public class EndUserRepository : EntityBaseRepository<EndUser>, IEndUserRepository
    {
        public EndUserRepository(SchedulerContext context)
            : base(context)
        { }
    }
}
