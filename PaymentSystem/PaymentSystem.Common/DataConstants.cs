namespace PaymentSystem.Common
{
    public static class DataConstants
    {
        public const int UserUsernameMinLength = 5;
        public const int UserUsernameMaxLength = 50;
        public const int UserPasswordMinLength = 5;
        public const int UserPasswordMaxLength = 50;

        public const int PaymentIBANLength = 22;
        public const int PaymentReasonMinLength = 3;
        public const int PaymentReasonMaxLength = 32;

        public const string Date = "Date";
        public const string Status = "Status";
        public const string DateOrder = "PaymentDate";
        public const string StatusOrder = "StatusId";

        public const string PaymentWaiting = "ИЗЧАКВА";
        public const string PaymentProcessed = "ОБРАБОТЕН";
        public const string PaymentCancelled = "ОТКАЗАН";
    }
}