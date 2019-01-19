using System;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Abstractions
{
    public class Vote
    {
        public ObjectId Id { get; set; }
        public ObjectId PersonId { get; set; }
        public ObjectId PollId { get; set; }
        public AntwortType Antwort { get; set; }
        
    }
    
    public enum AntwortType 
    { 
        Ja, 
        Nein 
    } 
    
    public interface IVoteRepository
    {
        Task<Vote> GetVote(ObjectId voteId);
        Task<Vote> GetVoteForPollAndPerson(ObjectId pollId, ObjectId personId);
        void AddVote(Vote aVote);
    }
}