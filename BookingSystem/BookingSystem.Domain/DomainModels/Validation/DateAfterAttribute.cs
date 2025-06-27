using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingSystem.Domain.DomainModels.Validation
{
    public class DateAfterAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateAfterAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (propertyInfo == null)
                return new ValidationResult($"Unknown property: {_comparisonProperty}");

            var comparisonValue = (DateTime)propertyInfo.GetValue(validationContext.ObjectInstance);

            if (value is DateTime date)
            {
                if (date.Date <= comparisonValue.Date)
                {
                    return new ValidationResult(ErrorMessage ?? $"Date must be after {_comparisonProperty}");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid date format");
        }
    }
}
