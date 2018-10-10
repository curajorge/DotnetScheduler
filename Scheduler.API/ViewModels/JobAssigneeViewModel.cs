using Scheduler.API.ViewModels.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Scheduler.API.ViewModels
{
    public class JobAssigneeViewModel //: IValidatableObject
    {
        public int Id { get; set; }
        public int EndUserId { get; set; }
        public int JobId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

       

        // public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //     var validator = new CustomerViewModelValidator();
        //     var result = validator.Validate(this);
        //     return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        //  }
    }
}
