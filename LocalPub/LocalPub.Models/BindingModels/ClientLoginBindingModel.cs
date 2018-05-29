using System.ComponentModel.DataAnnotations;
using static LocalPub.Common.MessageConstants;
using static LocalPub.Common.ModelConstants;

namespace LocalPub.Models.BindingModels
{
    public class ClientLoginBindingModel
    {
        [Required(ErrorMessage = Required)]
        [StringLength(UserUsernameMaxLength, ErrorMessage = FieldLengthErrorMessage, MinimumLength = UserUsernameMinLength)]
        public string Username { get; set; }

        [Required(ErrorMessage = Required)]
        [StringLength(UserPasswordMaxLength, ErrorMessage = FieldLengthErrorMessage, MinimumLength = UserPasswordMinLength)]
        public string Password { get; set; }
    }
}