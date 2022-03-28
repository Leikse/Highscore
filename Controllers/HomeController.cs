using Microsoft.AspNetCore.Mvc;
using Highscore.Data;
using Highscore.Models.Domain;
using Highscore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Highscore.Controllers;

public class HomeController : Controller
{
    public HomeController(HighscoreContext context)
    {
        Context = context;
    }

    private HighscoreContext Context { get; }

    public IActionResult Index()
    {
        var scoreList = new List<Score>();

        foreach (var item in Context.Game.ToList())
        {
            var scores = Context.Score
                .Include(score => score.Game)
                .Where(score => score.Game == item)
                .OrderByDescending(score => score.PlayerScore)
                .FirstOrDefault();

            scoreList.Add(scores);
        }

        var viewModel = new HomeIndexViewModel
        {
            Games = Context.Game.ToList(),
            Scores = scoreList
        };

        return View(viewModel);
    }
}
