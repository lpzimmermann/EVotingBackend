using System.ComponentModel.DataAnnotations;
using Abstractions;
using EVotingService.Validators;

namespace EVotingService.ServiceModels
{
    /// <summary>
    /// Stores data for creating a new Vote
    /// </summary>
    public class AddVoteModel
    {
        [Required]
        [ObjectIdValidator]
        public string PersonId { get; set; }
        
        [Required]
        [ObjectIdValidator]
        public string PollId { get; set; }
        
        [Required]
        [AntwortValidator]
        public AntwortType Antwort { get; set; }
    }
}