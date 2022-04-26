using Highscore.Data;
using Highscore.Models.Domain;
using Highscore.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Highscore.Areas.API.Controllers;

[Area("API")]
[Route("api/[controller]")]
[ApiController]
public class GamesController : ControllerBase
{
    public GamesController(HighscoreContext context)
    {
        Context = context;
    }

    private HighscoreContext Context { get; }

    public IEnumerable<GameDto> Index([FromQuery] string? name)
    {
        List<Game> games = string.IsNullOrEmpty(name)
            ? Context.Game.ToList()
            : Context.Game.Where(x => x.Name.Contains(name)).ToList();

        var gameDtos = games.Select(game => new GameDto
        {
            Id = game.Id,
            Name = game.Name,
        });

        return gameDtos;
    }
}
