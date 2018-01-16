using FluentValidation;
using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.API.Models
{
    [Validator(typeof(SignInValidator))]
    public class SignInModel
    {

        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }


    public class SignInValidator : AbstractValidator<SignInModel>
    {
        public SignInValidator()
        {
            RuleFor(x => x.Email).NotEmpty();

            /*
            RuleFor(x => x.LastName).NotEmpty().WithMessage("The Last Name cannot be blank.");

            RuleFor(x => x.BirthDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
            */
        }
    }

}