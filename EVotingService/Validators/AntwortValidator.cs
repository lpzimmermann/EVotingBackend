using System;
using System.ComponentModel.DataAnnotations;
using Abstractions;
using MongoDB.Bson;

namespace EVotingService.Validators
{
    public class AntwortValidator : ValidationAttribute
    {
        
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var parsed = Enum.IsDefined(typeof(AntwortType), value);

                if (!parsed)
                {
                    return new ValidationResult(GetErrorMessage());
                }

                return ValidationResult.Success;
            }

            private string GetErrorMessage()
            {
                return $"Answer is invalid.";
            }
        }
    }