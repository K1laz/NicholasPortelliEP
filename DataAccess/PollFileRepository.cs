using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Domain;

namespace DataAccess
{
    public class PollFileRepository : IPollRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "polls.json");

        public void CreatePoll(Poll poll)
        {
            List<Poll> polls = new List<Poll>();

            if (File.Exists(_filePath))
            {
                string json = File.ReadAllText(_filePath);
                polls = JsonSerializer.Deserialize<List<Poll>>(json) ?? new List<Poll>();
            }

            poll.Id = polls.Count > 0 ? polls.Max(p => p.Id) + 1 : 1;
            poll.DateCreated = DateTime.Now;

            polls.Add(poll);

            string updatedJson = JsonSerializer.Serialize(polls, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, updatedJson);
        }

        public IEnumerable<Poll> GetPolls()
        {
            if (!File.Exists(_filePath))
                return new List<Poll>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Poll>>(json) ?? new List<Poll>();
        }

        public void Vote(Poll poll)
        {
            var polls = GetPolls();
            var existing = polls.FirstOrDefault(p => p.Id == poll.Id);
            if (existing != null)
            {
                existing.Option1VotesCount = poll.Option1VotesCount;
                existing.Option2VotesCount = poll.Option2VotesCount;
                existing.Option3VotesCount = poll.Option3VotesCount;

                string json = JsonSerializer.Serialize(polls, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
        }
    }
}
