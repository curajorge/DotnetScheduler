using FluentValidation;

namespace Scheduler.API.ViewModels.Validations
{
    public class CustomerViewModelValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerViewModelValidator()
        {
            RuleFor(Customer => Customer.FirstName).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(Customer => Customer.LastName).NotEmpty().WithMessage("Name cannot be empty");
            
                       
        }
    }
}
