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

        /// <summary>
        /// Creates new instance of this class and assigns Injected classes
        /// </summary>
        /// <param name="voteCollection"></param>
        public VoteRepository(IMongoCollection<Vote> voteCollection)
        {
            _voteCollection = voteCollection;
        }

        /// <summary>
        /// Returns a Vote from the database
        /// </summary>
        /// <param name="voteId">A Vote ID</param>
        /// <returns>The requested Vote</returns>
        public async Task<Vote> GetVote(ObjectId voteId)
        {
            return await _voteCollection.AsQueryable().Where(vote => vote.Id == voteId).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Returns the Votes for a Person
        /// </summary>
        /// <param name="pollId">A Poll Id</param>
        /// <param name="personId">A Person Id</param>
        /// <returns>The requested Polls</returns>
        public async Task<Vote> GetVoteForPollAndPerson(ObjectId pollId, ObjectId personId)
        {
            return await _voteCollection.AsQueryable().Where(vote => vote.PersonId == personId).Where(vote => vote.PollId == pollId)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Adds a new Vote to the database
        /// </summary>
        /// <param name="aVote">A new Vote</param>
        public async void AddVote(Vote aVote)
        {
            await _voteCollection.InsertOneAsync(aVote);
        }
    }
}