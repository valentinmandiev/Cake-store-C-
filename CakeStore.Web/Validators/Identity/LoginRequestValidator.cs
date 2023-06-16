using CakeStore.Models.Request.Identity;
using FluentValidation;

namespace CakeStore.Web.Validators.Identity
{
    public class LoginRequestValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .WithMessage("UserName cannot be empty!");

            RuleFor(x => x.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("UserName cannot be empty!");

            RuleFor(x => x.Password.Length)
                .GreaterThan(5)
                .WithMessage("Password must be atleast 6 symbols!");
        }
    }
}
