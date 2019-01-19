using System.Threading.Tasks;
using Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Data
{
    public class VoteRepository : IVoteRepository
    {
        private readonly IMongoCollection<Vote> _voteCollection;

        public VoteRepository(IMongoCollection<Vote> voteCollection)
        {
            _voteCollection = voteCollection;
        }

        public async Task<Vote> GetVote(ObjectId voteId)
        {
            return await _voteCollection.AsQueryable().Where(vote => vote.Id == voteId).FirstOrDefaultAsync();
        }

        public async Task<Vote> GetVoteForPollAndPerson(ObjectId pollId, ObjectId personId)
        {
            return await _voteCollection.AsQueryable().Where(vote => vote.PersonId == personId).Where(vote => vote.PollId == pollId)
                .FirstOrDefaultAsync();
        }

        public async void AddVote(Vote aVote)
        {
            await _voteCollection.InsertOneAsync(aVote);
        }
    }
}