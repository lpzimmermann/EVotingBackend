using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Data
{
    public class PollRepository : IPollRepository
    {
        private readonly IMongoCollection<Poll> _pollCollection;

        /// <summary>
        /// Creates new instance of this class and assigns Injected classes
        /// </summary>
        /// <param name="pollCollection"></param>
        public PollRepository(IMongoCollection<Poll> pollCollection)
        {
            _pollCollection = pollCollection;
        }

        /// <summary>
        /// Returns a Poll from the database
        /// </summary>
        /// <param name="pollId">A Polls ID</param>
        /// <returns>The requested Poll</returns>
        public async Task<Poll> GetPoll(ObjectId pollId)
        {
            return await _pollCollection.AsQueryable().Where(poll => poll.Id.Equals(pollId))
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns the currently open Polls
        /// </summary>
        /// <returns>The requested Polls</returns>
        public async Task<List<Poll>> GetOpenPolls()
        {
            return await _pollCollection.AsQueryable().Where(poll => poll.Date > DateTime.Now).ToListAsync();
        }

        /// <summary>
        /// Adds a new Poll to the database
        /// </summary>
        /// <param name="aPoll">A new Poll</param>
        public async void AddPoll(Poll aPoll)
        {
            await _pollCollection.InsertOneAsync(aPoll);
        }
    }
}