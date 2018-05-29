using System.ComponentModel.DataAnnotations;
using PaymentSystem.Common;

namespace PaymentSystem.Models.BindingModels.Users
{
    public class UserLoginBindingModel
    {
        [Required(ErrorMessage = MessageConstants.RequiredError)]
        [StringLength(DataConstants.UserUsernameMaxLength, ErrorMessage = MessageConstants.PropertyLengthError, MinimumLength = DataConstants.UserUsernameMinLength)]
        public string Username { get; set; }

        [Required(ErrorMessage = MessageConstants.RequiredError)]
        [StringLength(DataConstants.UserPasswordMaxLength, ErrorMessage = MessageConstants.PropertyLengthError, MinimumLength = DataConstants.UserPasswordMinLength)]
        public string Password { get; set; }
    }
}