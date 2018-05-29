namespace BirthdaySystem.Common
{
    public static class MessageConstants
    {
        public const string RequiredError = "The {0} is required.";
        public const string PasswordsDoNotMatch = "The Password and Confirmation password do not match.";
        public const string PropertyLengthError = "The {0} must be at least {2} and less than {1} characters long.";

        public const string InvalidCredentials = "Invalid Credentials.";
        public const string RegistrationSuccessful = "Registration successful.";
        public const string ExistingUsernameError = "There is already a user with this username.";
        public const string LoginSuccessful = "Login successful.";
        public const string LogoutSuccessful = "Logout successful.";

        public const string PastDateError = "Birth Date should be in the past.";
        public const string MakeVotingError = "You cannot make new voting for this employee.";
        public const string MakeVotingSuccessful = "You have successfully created new voting.";
        public const string VoteError = "You cannot vote in this voting.";
        public const string VoteSuccessful = "You have successfully voted.";
        public const string CloseVotingError = "You cannot close this voting.";
        public const string CloseVotingSuccesful = "You have successfully closed the voting.";
    }
}