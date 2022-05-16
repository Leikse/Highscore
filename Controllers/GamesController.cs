using Highscore.Data;
using Highscore.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Highscore.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Highscore.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class GamesController : Controller
{
    public GamesController(HighscoreContext context)
    {
        Context = context;
    }

    private HighscoreContext Context { get; }

    [Route("Games/{urlSlug}")]
    public IActionResult Details(string urlSlug)
    {
        var scores = Context.Score
            .Include(score => score.Game)
            .Where(score => score.Game.Name == urlSlug)
            .OrderByDescending(game => game.PlayerScore)
            .Take(10)
            .ToList();

        Game game = Context.Game.FirstOrDefault(game => game.Name == urlSlug);

        var viewModel = new GamesIndexViewModel
        {
            Scores = scores,
            Game = game
        };

        return View(viewModel);
    }
}

