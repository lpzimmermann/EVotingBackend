using System;
using System.Linq;
using System.Threading.Tasks;
using Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IMongoCollection<Person> _personCollection;

        /// <summary>
        /// Creates new instance of this class and assigns Injected classes
        /// </summary>
        /// <param name="personCollection"></param>
        public PersonRepository(IMongoCollection<Person> personCollection)
        {
            _personCollection = personCollection;
        }

        /// <summary>
        /// Returns a Person from the database
        /// </summary>
        /// <param name="userId">A Person ID</param>
        /// <returns>The requested Person</returns>
        public async Task<Person> GetPerson(ObjectId userId)
        {
            var aPerson = await _personCollection.AsQueryable().Where(person => person.Passnummer == (userId)).FirstOrDefaultAsync();

            return aPerson;
        }

        /// <summary>
        /// Returns a Person from the database
        /// </summary>
        /// <param name="name">A Persons name</param>
        /// <returns>The requested Person</returns>
        public async Task<Person> GetPerson(string name)
        {
            return await _personCollection.AsQueryable().Where(person => person.Name.Equals(name))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Adds a Person to the database
        /// </summary>
        /// <param name="aPerson">A new Person</param>
        public async void AddPerson(Person aPerson)
        {
            await _personCollection.InsertOneAsync(aPerson);
        }
    }
}