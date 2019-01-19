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

        public PollRepository(IMongoCollection<Poll> pollCollection)
        {
            _pollCollection = pollCollection;
        }

        public async Task<Poll> GetPoll(ObjectId pollId)
        {
            return await _pollCollection.AsQueryable().Where(poll => poll.Id.Equals(pollId))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Poll>> GetOpenPolls()
        {
            return await _pollCollection.AsQueryable().Where(poll => poll.Date > DateTime.Now).ToListAsync();
        }

        public async void AddPoll(Poll aPoll)
        {
            await _pollCollection.InsertOneAsync(aPoll);
        }
    }
}