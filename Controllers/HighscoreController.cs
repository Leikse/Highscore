using Highscore.Data;
using Microsoft.AspNetCore.Mvc;
using Highscore.Models.ViewModels;

namespace Highscore.Controllers;

public class HighscoreController : Controller
{
    public HighscoreController(HighscoreContext context)
    {
        Context = context;
    }

    private HighscoreContext Context { get; }

    [Route("Games/{urlSlug")]
    public ActionResult Details(string urlSlug)
    {
        var highscore = Context.Score.FirstOrDefault(x => x.UrlSlug == urlSlug);
        var highscoreDetails = Context.Score.Take(4).ToList();

        var viewModel = new HighscoreDetailsViewModel
        {
            Highscore = highscore,
            HighscoreDetails = highscoreDetails
        };

        return View(viewModel);
    }
}

