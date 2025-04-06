using Microsoft.AspNetCore.Mvc;
using Domain;
using DataAccess;

namespace Presentation.Controllers
{
    public class PollController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(
            Poll poll,
            [FromServices] PollRepository dbRepo,
            [FromServices] PollFileRepository fileRepo)
        {
            if (ModelState.IsValid)
            {
                dbRepo.CreatePoll(poll);   // Save to DB
                fileRepo.CreatePoll(poll); // Save to JSON file
                return RedirectToAction("Success");
            }

            return View(poll);
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Index([FromServices] PollRepository repo)
        {
            var polls = repo.GetPolls()
                .OrderByDescending(p => p.DateCreated)
                .ToList();

            return View(polls);
        }

        public IActionResult Vote(int id, [FromServices] PollRepository repo)
        {
            var poll = repo.GetPolls().FirstOrDefault(p => p.Id == id);

            if (poll == null)
                return NotFound();

            return View(poll);
        }

        [HttpPost]
        public IActionResult SubmitVote(int id, int selectedOption, [FromServices] PollRepository repo)
        {
            var poll = repo.GetPolls().FirstOrDefault(p => p.Id == id);
            if (poll == null) return NotFound();
            
            // Increase vote count based on selected option
            switch (selectedOption)
            {
                case 1: poll.Option1VotesCount++; break;
                case 2: poll.Option2VotesCount++; break;
                case 3: poll.Option3VotesCount++; break;
                default: return BadRequest("Invalid option selected.");
            }

            repo.Vote(poll);
            return RedirectToAction("Success");
        }

    }
}
