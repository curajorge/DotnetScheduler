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
    public class AddressRepository : EntityBaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(SchedulerContext context)
            : base(context)
        { }
    }
}
