﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Domain;

namespace DataAccess
{
    public class PollFileRepository
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

        public List<Poll> GetPolls()
        {
            if (!File.Exists(_filePath))
                return new List<Poll>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Poll>>(json) ?? new List<Poll>();
        }
    }
}
