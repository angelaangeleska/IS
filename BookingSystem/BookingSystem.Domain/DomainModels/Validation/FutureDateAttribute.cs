using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.DomainModels.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime date)
            {
                if (date.Date < DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? "Date must be today or in the future");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid date format");
        }
    }
}
