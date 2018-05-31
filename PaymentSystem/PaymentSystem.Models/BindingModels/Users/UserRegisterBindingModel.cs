using PaymentSystem.Common;
using System.ComponentModel.DataAnnotations;

namespace PaymentSystem.Models.BindingModels.Users
{
    public class UserRegisterBindingModel
    {
        [Required(ErrorMessage = MessageConstants.RequiredError)]
        [StringLength(DataConstants.UserUsernameMaxLength, ErrorMessage = MessageConstants.PropertyLengthError, MinimumLength = DataConstants.UserUsernameMinLength)]
        public string Username { get; set; }

        [Required(ErrorMessage = MessageConstants.RequiredError)]
        [StringLength(DataConstants.UserPasswordMaxLength, ErrorMessage = MessageConstants.PropertyLengthError, MinimumLength = DataConstants.UserPasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = MessageConstants.PasswordsDoNotMatchError)]
        [Display(Name = DisplayConstants.ConfirmPasswordName)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = MessageConstants.RequiredError)]
        public string Name { get; set; }
    }
}