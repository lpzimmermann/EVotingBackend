using System;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Abstractions
{
    public class Person
    {
        [BsonId]
        public ObjectId Passnummer { get; set; }
        public string Name { get; set; }
        public string Vorname { get; set; }
        public DateTime Geburtstag { get; set; }
        
    }

    public interface IPersonRepository
    {
        Task<Person> GetPerson(ObjectId userId);
        Task<Person> GetPerson(String name);
        void AddPerson(Person aPerson);
    }
    
    
    
}