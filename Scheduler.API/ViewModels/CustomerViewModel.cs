using Scheduler.API.ViewModels.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Scheduler.API.ViewModels
{
    public class CustomerViewModel : IValidatableObject
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }                
        public string Email { get; set; }  
        //public string Lead { get; set; }
        public int LeadTypeId { get; set; }
        public LeadViewModel LeadType { get; set; }

        public ICollection<AddressViewModel> Addresses { get; set; }
        public ICollection<PhoneNumberViewModel> PhoneNumbers { get; set; }

        public string[] JobTypes { get; set; }
        public string[] LeadTypes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new CustomerViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
