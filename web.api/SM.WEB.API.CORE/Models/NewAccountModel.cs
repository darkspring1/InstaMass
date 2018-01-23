using FluentValidation;
using FluentValidation.Attributes;

namespace SM.WEB.API.Models
{

    [Validator(typeof(NewAccountModelValidator))]
    public class NewAccountModel
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }

    class NewAccountModelValidator : AbstractValidator<NewAccountModel>
    {
        public NewAccountModelValidator()
        {
            RuleFor(x => x.Login).NotEmpty().WithMessage("The Login cannot be blank.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("The Password cannot be blank.");
        }
    }
}
