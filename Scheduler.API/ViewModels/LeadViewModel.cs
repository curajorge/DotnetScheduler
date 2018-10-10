using Scheduler.API.ViewModels.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Scheduler.API.ViewModels
{
    public class LeadViewModel //: IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal WeeklyCost { get; set; }
        
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        // {
        // var validator = new CustomerViewModelValidator();
        // var result = validator.Validate(this);
        // return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        //  }
    }
}
