using System;
using System.ComponentModel.DataAnnotations;

namespace EVotingService.ServiceModels
{
    /// <summary>
    /// Stores data for creating a new User
    /// </summary>
    public class AddPersonModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Vorname { get; set; }
        [Required]
        public DateTime Geburtsdatum { get; set; }
    }
}