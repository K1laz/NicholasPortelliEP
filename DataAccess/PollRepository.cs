using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DataAccess
{
    public class PollRepository
    {
        private readonly PollDbContext _context;

        public PollRepository(PollDbContext context)
        {
            _context = context;
        }

        public void CreatePoll(string title, string option1Text, string option2Text, string option3Text, int option1VotesCount, int option2VotesCount, int option3VotesCount)
        {
            var poll = new Poll
            {
                Title = title,
                Option1Text = option1Text,
                Option2Text = option2Text,
                Option3Text = option3Text,
                Option1VotesCount = option1VotesCount,
                Option2VotesCount = option2VotesCount,
                Option3VotesCount = option3VotesCount,
                DateCreated = DateTime.Now
            };

            _context.Polls.Add(poll);
            _context.SaveChanges();
        }

        public void CreatePoll(Poll poll)
        {
            CreatePoll(
                poll.Title,
                poll.Option1Text,
                poll.Option2Text,
                poll.Option3Text,
                poll.Option1VotesCount,
                poll.Option2VotesCount,
                poll.Option3VotesCount
            );
        }

        public IQueryable<Poll> GetPolls()
        {
            return _context.Polls;
        }

        public void Vote(Poll poll)
        {
            _context.Polls.Update(poll);
            _context.SaveChanges();
        }


    }
}