using CarStockAPI.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarStockAPI.Filters
{
    public class Car_ValidYear: ValidationAttribute   
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var carAddDto = validationContext.ObjectInstance as CarAddDto;

            // When adding a new car, car's year must be valid year
            // Minimal value is 1886 - first car created time
            if (carAddDto != null) { 
                if (carAddDto.Year < 1886)
                {
                    return new ValidationResult("Year field is in valid.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
