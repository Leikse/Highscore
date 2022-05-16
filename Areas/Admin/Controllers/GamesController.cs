#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Highscore.Data;
using Highscore.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using Highscore.Areas.Admin.Models.ViewModels;

namespace Highscore.Areas.Admin.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Area("Admin")]
public class GamesController : Controller
{
    private readonly HighscoreContext _context;

    public GamesController(HighscoreContext context)
    {
        _context = context;
    }

    // GET: Admin/Games
    public async Task<IActionResult> Index()
    {
        return View(await _context.Game.ToListAsync());
    }

    // GET: Admin/Games/Create
    [Route("admin/games/new")]
    public IActionResult Create()
    {
        var viewModel = new CreateGameViewModel
        {
            GameItems = new()
            {
                new SelectListItem { Value = "puzzle", Text = "Puzzle" },
                new SelectListItem { Value = "shooter", Text = "Shooter" },
                new SelectListItem { Value = "arcade", Text = "Arcade" },
                new SelectListItem { Value = "maze", Text = "Maze" },
                new SelectListItem { Value = "strategy", Text = "Strategy" },
            }
        };

        return View(viewModel);
    }

    // POST: Admin/Games/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("admin/games/new")]
    public async Task<IActionResult> Create(CreateGameViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            var game = new Game
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Genre = viewModel.Genre,
                ReleaseYear = viewModel.ReleaseYear,
                ImageUrl = viewModel.ImageUrl,
                UrlSlug = viewModel.Name.Replace("-", "").Replace(" ", "-").ToLower()
            };

            _context.Add(game);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Games");
        }

        viewModel.GameItems = new()
        {
            new SelectListItem { Value = "puzzle", Text = "Puzzle" },
            new SelectListItem { Value = "shooter", Text = "Shooter" },
            new SelectListItem { Value = "arcade", Text = "Arcade" },
            new SelectListItem { Value = "maze", Text = "Maze" },
            new SelectListItem { Value = "strategy", Text = "Strategy" },
        };

        return View(viewModel);
    }
}
