using System;
using System.Threading.Tasks;
using Abstractions;
using EVotingService.ServiceModels;
using EVotingService.Validators;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Swashbuckle.AspNetCore.Swagger;

namespace EVotingService.Controllers
{
    /// <summary>
    /// Controller for the Validation resources
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Initializes this class
        /// </summary>
        /// <param name="personRepository"></param>
        public ValidateController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        /// Checks whether the Person with the given id's birth date matches with the given
        /// </summary>
        /// <param name="aValidatePersonModel"></param>
        /// <returns>The requested Person</returns>
        /// <response code="200">If the birthdays do match</response>
        /// <response code="400">If the birthdays don't match</response> 
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Person>> Validate(ValidatePersonModel aValidatePersonModel)
        {
            Person aPerson = await _personRepository.GetPerson(ObjectId.Parse(aValidatePersonModel.Passnummer));
            if (aPerson != null && aPerson.Geburtstag.Date.Equals(aValidatePersonModel.Geburtsdatum.Date))
            {
                return Ok(aPerson);
            }  
            return BadRequest();
        }
    }
}