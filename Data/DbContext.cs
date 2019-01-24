using Abstractions;
using MongoDB.Driver;
using Serilog;

namespace Data
{
    internal class DbContext
    {

        private readonly IMongoDatabase _db;

        public DbContext(IMongoDatabase db)
        {
            _db = db;
            SetupIndexes();
        }
        
        public virtual IMongoCollection<Person> Persons => _db.GetCollection<Person>("person");
        public virtual IMongoCollection<Poll> Polls => _db.GetCollection<Poll>("poll");
        public virtual IMongoCollection<Vote> Votes => _db.GetCollection<Vote>("vote");
        
        /// <summary>
        /// Sets up the database indexes for collections
        /// </summary>
        private void SetupIndexes()
        {
            Log.Debug("Setting up Indexes");

            IndexKeysDefinitionBuilder<Person> notificationLogBuilder = Builders<Person>.IndexKeys;
            IndexKeysDefinitionBuilder<Poll> pollNotificationLogBuilder = Builders<Poll>.IndexKeys;
            IndexKeysDefinitionBuilder<Vote> voteNotificationLogBuilder = Builders<Vote>.IndexKeys;
            
            CreateIndexModel<Person>[] indexModel = new[]
            {
                new CreateIndexModel<Person>(notificationLogBuilder.Ascending( _=>_ .Passnummer)),
                new CreateIndexModel<Person>(notificationLogBuilder.Ascending( _=>_ .Name))
            };
            CreateIndexModel<Poll>[] pollIndexModel = new[]
            {
                new CreateIndexModel<Poll>(pollNotificationLogBuilder.Ascending(_ => _.Id)),
                new CreateIndexModel<Poll>(pollNotificationLogBuilder.Ascending(_ => _.Title))
            };
            CreateIndexModel<Vote>[] voteIndexModel = new[]
            {
                new CreateIndexModel<Vote>(voteNotificationLogBuilder.Ascending(_ => _.Id))
            };
            Persons.Indexes.CreateMany(indexModel);
            Polls.Indexes.CreateMany(pollIndexModel);
            Votes.Indexes.CreateMany(voteIndexModel);
            Log.Debug("Index setup finished");
        }
        
    }
}