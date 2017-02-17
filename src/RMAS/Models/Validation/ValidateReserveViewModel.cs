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
            var list = value as IList<DateTime>;
            if (list != null)
            {
                foreach (DateTime date in list)
                {
                    if ((DateTime)date < DateTime.Today || (DateTime)date > Convert.ToDateTime("12/31/9999"))
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
