using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions;
using EVotingService.ServiceModels;
using EVotingService.Validators;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Serilog;
using Serilog.Events;
using Swashbuckle.AspNetCore.Swagger;

namespace EVotingService.Controllers
{
    /// <summary>
    /// Controller for the Person resources
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        /// <summary>
        /// Initializes this class
        /// </summary>
        /// <param name="personRepository"></param>
        public PersonsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        /// Returns the Person with the given Id
        /// </summary>
        /// <param name="personId"></param>
        /// <returns>The requested Person</returns>
        /// <response code="200">If the Person exists</response>
        /// <response code="400">If the Person doesn't exist</response>
        [HttpGet("{personId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Person>> Get([ObjectIdValidator] String personId)
        {
            Person aPerson = await _personRepository.GetPerson(ObjectId.Parse(personId));
            if (aPerson == null)
            {
                return BadRequest();
            }
           return Ok(aPerson);
        }

        /// <summary>
        /// Adds a new Person to the database
        /// </summary>
        /// <param name="anAddPersonModel"></param>
        /// <response code="204">If the operation was successful</response>
        [HttpPut]
        [ProducesResponseType(204)]
        public void Add(AddPersonModel anAddPersonModel)
        {
            _personRepository.AddPerson(new Person()
            {
                Geburtstag = anAddPersonModel.Geburtsdatum,
                Name = anAddPersonModel.Name,
                Vorname = anAddPersonModel.Vorname,
                Passnummer = ObjectId.GenerateNewId()
            });
        }
        
    }
}