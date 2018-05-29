using System.ComponentModel.DataAnnotations;
using static PizzaLab.Common.Constants;
using static PizzaLab.Common.Messages;

namespace PizzaLab.Models.BindingModels
{
    public class LoginUserBindingModel
    {
        [Required(ErrorMessage = UsernameRequired)]
        [StringLength(UserUsernameMaxLength, ErrorMessage = FieldLengthErrorMessage, MinimumLength = UserUsernameMinLength)]
        public string Username { get; set; }
        
        [Required(ErrorMessage = PasswordRequired)]
        [StringLength(UserPasswordMaxLength, ErrorMessage = FieldLengthErrorMessage, MinimumLength = UserPasswordMinLength)]
        public string Password { get; set; }
    }
}