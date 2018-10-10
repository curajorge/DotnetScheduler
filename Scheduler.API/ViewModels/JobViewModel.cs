using System;
using System.Collections.Generic;

namespace Scheduler.API.ViewModels
{
    public class JobViewModel //: IValidatableObject
    {
        public int Id { get; set; }        
        public DateTime Date { get; set; }
        public string JobType { get; set; }
        public string Status { get; set; }
        public int CustomerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int AddressId { get; set; }



        public ICollection<WorkDescriptionViewModel> WorkDescriptions { get; set; }
        public ICollection<JobAssigneeViewModel> JobAssignees { get; set; }
        //public ICollection<AddressViewModel> Addresses { get; set; }
        public AddressViewModel Address { get; set; }

        public string[] Types { get; set; }


        //Validator ToDO
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //   var validator = new CustomerViewModelValidator();
        //  var result = validator.Validate(this);
        // return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        // }
    }
}
