using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Abstractions
{
    public class Poll
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }

    public interface IPollRepository
    {
        Task<Poll> GetPoll(ObjectId pollId);
        Task<List<Poll>> GetOpenPolls();
        void AddPoll(Poll aPoll);
    }
}