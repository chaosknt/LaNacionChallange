using System;
using System.ComponentModel.DataAnnotations;

namespace LaNacion.Common.Helpers.Attribute
{
    public class ValidateBirthDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime birthDate = (DateTime)value;
            DateTime minDate = DateTime.Now.AddYears(-100);
            DateTime maxDate = DateTime.Now;

            if (birthDate < minDate || birthDate > maxDate)
            {
                return new ValidationResult("The provided Date is invalid");
            }

            return ValidationResult.Success;
        }
    }
}
