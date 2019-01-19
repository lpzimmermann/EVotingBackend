using System;
using System.ComponentModel.DataAnnotations;

namespace EVotingService.ServiceModels
{
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