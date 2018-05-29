using BirthdaySystem.Models.ValidationAttributes;
using System;
using System.ComponentModel.DataAnnotations;
using static BirthdaySystem.Common.DataConstants;
using static BirthdaySystem.Common.DisplayConstants;
using static BirthdaySystem.Common.MessageConstants;

namespace BirthdaySystem.Models.BindingModels.Employees
{
    public class EmployeeRegisterBindingModel
    {
        [Required(ErrorMessage = RequiredError)]
        [StringLength(UserUsernameMaxLength, ErrorMessage = PropertyLengthError, MinimumLength = UserUsernameMinLength)]
        public string Username { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [StringLength(UserPasswordMaxLength, ErrorMessage = PropertyLengthError, MinimumLength = UserPasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = PasswordsDoNotMatch)]
        [Display(Name = ConfirmPasswordName)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [PastDate]
        public string Name { get; set; }

        [Required(ErrorMessage = RequiredError)]
        [Display(Name = BirthDateName)]
        [DataType(DataType.Date)]
        [PastDate]
        public DateTime BirthDate { get; set; }
    }
}