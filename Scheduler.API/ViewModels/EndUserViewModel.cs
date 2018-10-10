using System;
using System.Collections.Generic;

namespace Scheduler.API.ViewModels
{
    public class EndUserViewModel //: IValidatableObject
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public int NumberOfJobs { get; set; }

        public ICollection<JobAssigneeViewModel> JobAssigees { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastLogging { get; set; }
        //public ICollection<JobViewModel> Jobs { get; set; }

        public string[] Types { get; set; }


        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        // {
        //    var validator = new CustomerViewModelValidator();
        //var result = validator.Validate(this);
        //return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        //   }
    }
}
