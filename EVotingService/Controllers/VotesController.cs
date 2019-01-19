using System;
using System.Threading.Tasks;
using Abstractions;
using EVotingService.ServiceModels;
using EVotingService.Validators;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EVotingService.Controllers
{
    /// <summary>
    /// Controller for the Vote resources
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class VotesController : ControllerBase
    {

        private readonly IVoteRepository _voteRepository;

        /// <summary>
        /// Initializes this class
        /// </summary>
        /// <param name="voteRepository"></param>
        public VotesController(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        /// <summary>
        /// Returns the Vote with the given Id
        /// </summary>
        /// <param name="aVoteId"></param>
        /// <returns>The requested Vote</returns>
        /// <response code="200">If Vote exists</response>
        /// <response code="400">If Vote doesn't exist</response>
        [HttpGet("{aVoteId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Vote>> Get([ObjectIdValidator] string aVoteId)
        {
            Vote aVote = await _voteRepository.GetVote(ObjectId.Parse(aVoteId));

            if (aVote == null)
            {
                return BadRequest();
            }
           return Ok(aVote);
        }
    
        /// <summary>
        /// Adds a new Vote to the database
        /// </summary>
        /// <param name="anAddVoteModel"></param>
        /// <response code="200">If the operation was successful</response>
        [HttpPut]
        [ProducesResponseType(204)]
        public void Add(AddVoteModel anAddVoteModel)
        {
            _voteRepository.AddVote(new Vote()
            {
                Antwort = anAddVoteModel.Antwort,
                Id = ObjectId.GenerateNewId(),
                PersonId = ObjectId.Parse(anAddVoteModel.PersonId),
                PollId = ObjectId.Parse(anAddVoteModel.PollId)
            });
        }
    }
}