using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RMAS.Models.Validation
{
    public class ValidateSearchDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {            
            if (value == null || ((DateTime)value >= Convert.ToDateTime("01/01/1900") && (DateTime)value <= Convert.ToDateTime("12/31/9999")))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date is not in given range.");
            }
        }
    }
}
