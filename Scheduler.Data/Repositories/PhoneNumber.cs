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
    public class PhoneNumberRepository : EntityBaseRepository<PhoneNumber>, IPhoneNumberRepository
    {
        public PhoneNumberRepository(SchedulerContext context)
            : base(context)
        { }
    }
}
