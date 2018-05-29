namespace LocalPub.Common
{
    public static class MessageConstants
    {
        public const string Required = "The {0} is required.";
        public const string PasswordsDoNotMatch = "The Password and Confirmation password do not match.";
        public const string FieldLengthErrorMessage = "The {0} must be at least {2} and less than {1} characters long.";

        public const string UserExistingUsername = "There is already a user with the same username.";
        public const string InvalidCredentials = "Invalid Credentials.";
        public const string RegistrationSuccessful = "Registration successful.";
        public const string LoginSuccessful = "Login successful.";
        public const string LogoutSuccessful = "Logout successful.";

        public const string CancelSuccessful = "You have successfully cancelled your order.";
        public const string CancelError = "Something happened, please try again.";
        public const string OrderSuccessful = "Thank you for your order.";
        public const string OrderError = "You have to choose at least one meal.";
    }
}