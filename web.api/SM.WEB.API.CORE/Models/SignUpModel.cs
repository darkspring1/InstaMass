using FluentValidation;
using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace AngularJSAuthentication.API.Models
{
    

    [Validator(typeof(SignUpValidator))]
    public class SignUpModel : SignInModel
    {

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }


    public class SignUpValidator : AbstractValidator<SignUpModel>
    {
        public SignUpValidator()
        {
            RuleFor(x => x.Email).NotEmpty();

            RuleFor(x => x.UserName).NotEmpty().WithMessage("The User Name cannot be blank.")
                                        .Length(0, 100).WithMessage("The First Name cannot be more than 100 characters.");
            /*
            RuleFor(x => x.LastName).NotEmpty().WithMessage("The Last Name cannot be blank.");

            RuleFor(x => x.BirthDate).LessThan(DateTime.Today).WithMessage("You cannot enter a birth date in the future.");

            RuleFor(x => x.Username).Length(8, 999).WithMessage("The user name must be at least 8 characters long.");
            */
        }
    }

}