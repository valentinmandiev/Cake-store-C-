using CakeStore.Models.Request.Factory;
using FluentValidation;

namespace CakeStore.Web.Validators.Factory
{
    public class UpdateFactoryValidator : AbstractValidator<UpdateFactoryRequest>
    {
        public UpdateFactoryValidator()
        {
            RuleFor(x => x.Name)
               .NotNull()
               .NotEmpty()
               .WithMessage("Name cannot be empty!");

            RuleFor(x => x.EIK)
                .Equal(10)
                .WithMessage("Eik must be 10 digits!");

            RuleFor(x => x.Address)
                .NotNull()
                .NotEmpty()
                .WithMessage("Address cannot be empty!");
        }
    }
}
