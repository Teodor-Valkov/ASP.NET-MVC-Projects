namespace PaymentSystem.Common
{
    public static class MessageConstants
    {
        public const string RequiredError = "The {0} is required.";
        public const string PropertyLengthError = "The {0} must be at least {2} and less than {1} characters long.";

        public const string PaymentIBANError = "The {0} must contain only English letters or digits.";
        public const string PaymentIBANLengthError = "The {0} must be exactly 22 characters long.";
        public const string PaymentAmountError = "The {0} must be a positive number with exactly 2 digits after the decimal point.";
        public const string PaymentReasonLengthError = "The {0} must be at least {2} and less than {1} characters long.";

        public const string InvalidCredentials = "Invalid Credentials.";
        public const string LoginSuccessful = "Login successful.";
        public const string LogoutSuccessful = "Logout successful.";
        public const string RegistrationSuccessful = "Registration successful.";
        public const string ExistingUsernameError = "There is already a user with this username.";
        public const string PasswordsDoNotMatchError = "The Password and Confirm Password do not match.";

        public const string AccountNotFoundError = "The specified account was not found.";
        public const string PaymentNotFoundError = "The specified payment was not found.";
        public const string AccountNotEnoughAmountError = "The amount in your account is not enough for this payment.";

        public const string MakePaymentError = "You cannot make the specified payment.";
        public const string MakePaymentSuccess = "You have successfully made new payment.";

        public const string ProcessPaymentError = "You cannot process the specified payment.";
        public const string ProcessPaymentSuccess = "You have successfully processed your payment.";

        public const string CancelPaymentError = "You cannot cancel the specified payment.";
        public const string CancelPaymentSuccess = "You have successfully cancelled your payment.";
    }
}