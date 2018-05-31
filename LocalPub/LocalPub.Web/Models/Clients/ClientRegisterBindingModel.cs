using System.ComponentModel.DataAnnotations;
using static LocalPub.Common.DisplayConstants;
using static LocalPub.Common.ModelConstants;
using static LocalPub.Common.MessageConstants;

namespace LocalPub.Web.Models.Clients
{
    public class ClientRegisterBindingModel
    {
        [Required(ErrorMessage = Required)]
        [StringLength(UserUsernameMaxLength, ErrorMessage = FieldLengthErrorMessage, MinimumLength = UserUsernameMinLength)]
        public string Username { get; set; }

        [Required(ErrorMessage = Required)]
        [StringLength(UserPasswordMaxLength, ErrorMessage = FieldLengthErrorMessage, MinimumLength = UserPasswordMinLength)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = PasswordsDoNotMatch)]
        [Display(Name = ConfirmPasswordName)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = Required)]
        public string Name { get; set; }

        [Required(ErrorMessage = Required)]
        [Display(Name = ClientTypeName)]
        public int ClientTypeId { get; set; }

        public System.Web.Mvc.SelectList ClientTypesSelectList { get; set; }
    }
}