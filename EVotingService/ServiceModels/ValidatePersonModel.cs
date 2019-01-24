using System;
using System.ComponentModel.DataAnnotations;
using EVotingService.Validators;

namespace EVotingService.ServiceModels
{
    /// <summary>
    /// Stores data for validating a Person
    /// </summary>
    public class ValidatePersonModel
    {
        [Required]
        [ObjectIdValidator]
        public string Passnummer { get; set; }
        [Required]
        public DateTime Geburtsdatum { get; set; }
    }
}