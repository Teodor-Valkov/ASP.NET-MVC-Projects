using System.ComponentModel.DataAnnotations;
using static PizzaLab.Common.Constants;
using static PizzaLab.Common.Messages;

namespace PizzaLab.Models.BindingModels
{
    public class RegisterUserBindingModel
    {
        [Required(ErrorMessage = UsernameRequired)]
        [StringLength(UserUsernameMaxLength, ErrorMessage = FieldLengthErrorMessage, MinimumLength = UserUsernameMinLength)]
        public string Username { get; set; }

        [Required(ErrorMessage = PasswordRequired)]
        [StringLength(UserPasswordMaxLength, ErrorMessage = FieldLengthErrorMessage, MinimumLength = UserPasswordMinLength)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = PasswordsDoNotMatch)]
        [Display(Name = ConfirmPasswordName)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = AddressRequired)]
        public string Address { get; set; }

        [Required(ErrorMessage = PhoneRequired)]
        public string Phone { get; set; }
    }
}
