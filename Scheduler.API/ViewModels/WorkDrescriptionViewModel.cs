using Scheduler.API.ViewModels.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Scheduler.API.ViewModels
{
    public class WorkDescriptionViewModel //: IValidatableObject
    {
        public int Id { get; set; }        
        public string WorkType { get; set; }
        public decimal Revenue { get; set; }
        
        

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        // {
        // var validator = new CustomerViewModelValidator();
        // var result = validator.Validate(this);
        // return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        //  }
    }
}
