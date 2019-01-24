using System;
using System.ComponentModel.DataAnnotations;

namespace EVotingService.ServiceModels
{
    /// <summary>
    /// Stores data for adding a new Poll
    /// </summary>
    public class AddPollModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime PollDate { get; set; }
    }
}