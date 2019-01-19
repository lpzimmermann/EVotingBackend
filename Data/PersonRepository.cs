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

        public PersonRepository(IMongoCollection<Person> personCollection)
        {
            _personCollection = personCollection;
        }

        public async Task<Person> GetPerson(ObjectId userId)
        {
            var aPerson = await _personCollection.AsQueryable().Where(person => person.Passnummer == (userId)).FirstOrDefaultAsync();

            return aPerson;
        }

        public async Task<Person> GetPerson(string name)
        {
            return await _personCollection.AsQueryable().Where(person => person.Name.Equals(name))
                .FirstOrDefaultAsync();
        }

        public async void AddPerson(Person aPerson)
        {
            await _personCollection.InsertOneAsync(aPerson);
        }
    }
}