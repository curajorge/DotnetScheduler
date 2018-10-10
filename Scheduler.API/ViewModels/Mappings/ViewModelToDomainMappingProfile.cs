using AutoMapper;
using Scheduler.Model;
using System.Collections.Generic;

namespace Scheduler.API.ViewModels.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ScheduleViewModel, Schedule>()
               .ForMember(s => s.Creator, map => map.UseValue(null))
               .ForMember(s => s.Attendees, map => map.UseValue(new List<Attendee>()));

            Mapper.CreateMap<UserViewModel, User>();

            Mapper.CreateMap<AddressViewModel, Address>();

            Mapper.CreateMap<PhoneNumberViewModel, PhoneNumber>();

            Mapper.CreateMap<CustomerViewModel, Customer>();

            Mapper.CreateMap<Job, JobViewModel>();
            //.ForMember(j => j.Address, map => map.UseValue(null))
            //.ForMember(j => j.Address, map => map.UseValue(null));


            Mapper.CreateMap<EndUser, EndUserViewModel>();
                

            Mapper.CreateMap<JobAssignee, JobAssigneeViewModel>();
                   

            Mapper.CreateMap<WorkDescription, WorkDescriptionViewModel>();

        }
    }
}
