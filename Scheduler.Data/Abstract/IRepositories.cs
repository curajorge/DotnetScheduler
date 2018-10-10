using Scheduler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Data.Abstract
{
    public interface IScheduleRepository : IEntityBaseRepository<Schedule> { }

    public interface IUserRepository : IEntityBaseRepository<User> { }

    public interface IAttendeeRepository : IEntityBaseRepository<Attendee> { }

    //Freezer
    public interface ICustomerRepository : IEntityBaseRepository<Customer> { }

    public interface IPhoneNumberRepository : IEntityBaseRepository<PhoneNumber> { }

    public interface IAddressRepository : IEntityBaseRepository<Address> { }

    public interface IJobRepository : IEntityBaseRepository<Job> { }

    public interface IEndUserRepository : IEntityBaseRepository<EndUser> { }

    public interface IWorkDescriptionRepository : IEntityBaseRepository<WorkDescription> { }

    public interface IJobAssigneeRepository : IEntityBaseRepository<JobAssignee> { }

    public interface ILeadRepository : IEntityBaseRepository<Lead> { }





}
