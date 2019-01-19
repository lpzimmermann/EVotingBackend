using System;
using System.ComponentModel.DataAnnotations;

namespace EVotingService.ServiceModels
{
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