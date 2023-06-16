using CakeStore.Models.Request.Cake;
using FluentValidation;

namespace CakeStore.Web.Validators.Cake
{
    public class UpdateCakeValidator : AbstractValidator<UpdateCakeRequest>
    {
        public UpdateCakeValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name cannot be empty!");
            RuleFor(x => x.FactoryId)
                .NotEmpty()
                .NotEmpty()
                .WithMessage("FactoryId cannot be empty!");
        }
    }
}
