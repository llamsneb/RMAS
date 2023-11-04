using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RMAS.Models.ReserveViewModels;

namespace RMAS.Models.Validation
{
    public class ValidateReserveDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = value as IList<DateOnly>;
            if (list != null)
            {
                foreach (DateOnly date in list)
                {
                    if ((DateOnly)date < DateOnly.FromDateTime(DateTime.Now) || (DateOnly)date > DateOnly.MaxValue)
                    {
                        return new ValidationResult("Date is not in given range.");
                    }
                }
            }
            else
            {
                return new ValidationResult("Date is required.");
            }
            return ValidationResult.Success;
        }
    }

    public class ValidateReserveTime : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ReserveViewModel obj = (ReserveViewModel)value;
            if (obj.BeginTime != null && obj.EndTime != null)
            {
                if (obj.BeginTime >= obj.EndTime)
                {
                    return new ValidationResult("Begin time must be prior to end time.");
                }
            }
            else
            {
                return new ValidationResult("Begin Time and End Time are required.");
            }

            return ValidationResult.Success;
        }
    }
}
