using AutoMapper;
using Scheduler.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scheduler.API.ViewModels.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Schedule, ScheduleViewModel>()
               .ForMember(vm => vm.Creator,
                    map => map.MapFrom(s => s.Creator.Name))
               .ForMember(vm => vm.Attendees, map =>
                    map.MapFrom(s => s.Attendees.Select(a => a.UserId)));

            Mapper.CreateMap<Schedule, ScheduleDetailsViewModel>()
               .ForMember(vm => vm.Creator,
                    map => map.MapFrom(s => s.Creator.Name))
               .ForMember(vm => vm.Attendees, map =>
                    map.UseValue(new List<UserViewModel>()))
                .ForMember(vm => vm.Status, map =>
                    map.MapFrom(s => ((ScheduleStatus)s.Status).ToString()))
                .ForMember(vm => vm.Type, map =>
                   map.MapFrom(s => ((ScheduleType)s.Type).ToString()))
               .ForMember(vm => vm.Statuses, map =>
                    map.UseValue(Enum.GetNames(typeof(ScheduleStatus)).ToArray()))
               .ForMember(vm => vm.Types, map =>
                    map.UseValue(Enum.GetNames(typeof(ScheduleType)).ToArray()));

            Mapper.CreateMap<User, UserViewModel>()
                .ForMember(vm => vm.SchedulesCreated,
                    map => map.MapFrom(u => u.SchedulesCreated.Count()));

            Mapper.CreateMap<Customer, CustomerViewModel>()
                .ForMember(vm => vm.JobTypes, map =>
                map.UseValue(Enum.GetNames(typeof(JobType)).ToArray()))
                .ForMember(vm => vm.LeadTypes, map =>
                map.UseValue(Enum.GetNames(typeof(LeadType)).ToArray()));


            Mapper.CreateMap<Address, AddressViewModel>();

            Mapper.CreateMap<PhoneNumber, PhoneNumberViewModel>();

            Mapper.CreateMap<Job, JobViewModel>()
                 .ForMember(vm => vm.Types, map =>
                map.UseValue(Enum.GetNames(typeof(JobType)).ToArray()))
                .ForMember(vm => vm.FirstName, map => map.MapFrom(u => u.Customer.FirstName))
                .ForMember(vm => vm.LastName, map => map.MapFrom(u => u.Customer.LastName));

            Mapper.CreateMap<EndUser, EndUserViewModel>()
                .ForMember(vm => vm.NumberOfJobs,
                    map => map.MapFrom(u => u.JobAssignees.Count()))
                .ForMember(vm => vm.Type, map =>
                   map.MapFrom(s => ((UserType)s.Type).ToString()))
               
               .ForMember(vm => vm.Types, map =>
                    map.UseValue(Enum.GetNames(typeof(UserType)).ToArray()));

            Mapper.CreateMap<JobAssignee, JobAssigneeViewModel>()
                .ForMember(vm => vm.FirstName, map => map.MapFrom(u => u.EndUser.FirstName))
                .ForMember(vm => vm.LastName, map => map.MapFrom(u => u.EndUser.LastName));


            Mapper.CreateMap<WorkDescription, WorkDescriptionViewModel>();

            Mapper.CreateMap<Lead, LeadViewModel>();

            


            //Mapper.CreateMap<Job, TypesViewModel>()









        }
    }
}
