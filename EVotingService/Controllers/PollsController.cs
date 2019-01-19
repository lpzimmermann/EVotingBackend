using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions;
using EVotingService.ServiceModels;
using EVotingService.Validators;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EVotingService.Controllers
{
    /// <summary>
    /// Controller for the Poll resources
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PollsController : ControllerBase
    {

        private readonly IPollRepository _pollRepository;
        private readonly IVoteRepository _voteRepository;

        /// <summary>
        /// Initializes this class
        /// </summary>
        /// <param name="pollRepository"></param>
        /// <param name="voteRepository"></param>
        public PollsController(IPollRepository pollRepository, IVoteRepository voteRepository)
        {
            _pollRepository = pollRepository;
            _voteRepository = voteRepository;
        }

        /// <summary>
        /// Returns the Poll with the given Id
        /// </summary>
        /// <param name="aPollId"></param>
        /// <returns>The requested Poll</returns>
        /// <response code="200">If the Poll exists</response>
        /// <response code="204">If the Poll doesn't exist</response>
        [HttpGet("{aPollId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Poll>> Get([ObjectIdValidator] string aPollId)
        {
            Poll aPoll = await _pollRepository.GetPoll(ObjectId.Parse(aPollId));
            if (aPoll == null)
            {
                return BadRequest();
            }
            return Ok(aPoll);
        }

        /// <summary>
        /// Returns the available Polls for a Person
        /// </summary>
        /// <param name="aPersonId"></param>
        /// <returns>A List of Polls</returns>
        /// <response code="200">If the operation was successful</response>
        [HttpGet("person/{aPersonId}")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Poll>>> GetPollsForUser([ObjectIdValidator] string aPersonId)
        {

            List<Poll> tempPolls = await _pollRepository.GetOpenPolls();
            List<Poll> resultPoll = new List<Poll>();
            
            foreach (var poll in tempPolls)
            {
                Vote vote = await _voteRepository.GetVoteForPollAndPerson(poll.Id, ObjectId.Parse(aPersonId));
                if (vote == null)
                {
                    resultPoll.Add(poll);
                }
            }
            
            return Ok(resultPoll);
        }

        /// <summary>
        /// Adds a new Poll to the database
        /// </summary>
        /// <param name="anAddPollModel"></param>
        /// <response code="204">If the operation was successful</response>
        [HttpPut]
        [ProducesResponseType(204)]
        public void Add(AddPollModel anAddPollModel)
        {
            _pollRepository.AddPoll(new Poll()
            {
                Date = anAddPollModel.PollDate,
                Description = anAddPollModel.Description,
                Id = ObjectId.GenerateNewId(),
                Title = anAddPollModel.Title
            });
        }
    }
}