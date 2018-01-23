using FluentValidation;
using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API.Models
{
    [Validator(typeof(UserValidator))]
    public class UserModel
    {

        public string Email { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }


    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
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