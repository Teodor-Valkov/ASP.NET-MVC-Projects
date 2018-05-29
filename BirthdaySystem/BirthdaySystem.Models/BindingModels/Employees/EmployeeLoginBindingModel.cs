using System.ComponentModel.DataAnnotations;
using static BirthdaySystem.Common.DataConstants;
using static BirthdaySystem.Common.MessageConstants;

namespace BirthdaySystem.Models.BindingModels.Employees
{
    public class EmployeeLoginBindingModel
    {
        [Required(ErrorMessage = RequiredError)]
        [StringLength(UserUsernameMaxLength, ErrorMessage = PropertyLengthError, MinimumLength = UserUsernameMinLength)]
        public string Username { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [StringLength(UserPasswordMaxLength, ErrorMessage = PropertyLengthError, MinimumLength = UserPasswordMinLength)]
        public string Password { get; set; }
    }
}