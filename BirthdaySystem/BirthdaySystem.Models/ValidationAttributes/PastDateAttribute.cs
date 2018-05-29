using System;
using System.ComponentModel.DataAnnotations;
using static BirthdaySystem.Common.MessageConstants;

namespace BirthdaySystem.Models.ValidationAttributes
{
    public class PastDateAttribute : ValidationAttribute
    {
        public PastDateAttribute()
        {
            this.ErrorMessage = PastDateError;
        }

        public override bool IsValid(object value)
        {
            DateTime? date = value as DateTime?;

            if (date == null)
            {
                return true;
            }

            if (date.Value.Date >= DateTime.UtcNow.Date)
            {
                return false;
            }

            return true;
        }
    }
}