using Highscore.Areas.API.V2.Models;
using Highscore.Data;
using Highscore.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Highscore.Areas.API.V2.Controllers;

[ApiVersion("2")]
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
        var games = string.IsNullOrEmpty(name)
            ? Context.Game.ToList()
            : Context.Game.Where(x => x.Name.Contains(name)).ToList();

        var gameDtos = games.Select(game => new GameDto
        {
            Id = game.Id,
            Name = game.Name,
            ReleaseYear = game.ReleaseYear,
            Genre = game.Genre,
            Description = game.Description
        });

        return gameDtos;
    }

    [HttpGet("{id}", Name = "GetGame")]
    public ActionResult<GameDto> GetGame(int id)
    {
        var game = Context.Game.FirstOrDefault(x => x.Id == id);

        if (game == null)
        {
            return NotFound();
        }

        var gameDto = new GameDto
        {
            Id = id,
            Name = game.Name
        };
        return gameDto;
    }

    [HttpPost]
    public IActionResult CreateGame(CreateGameDto createGameDto)
    {
        var game = new Game
        {
            Name = createGameDto.Name,
            Description = createGameDto.Description,
            ReleaseYear = createGameDto.ReleaseYear,
            Genre = createGameDto.Genre,
            ImageUrl = createGameDto.ImageUrl,
            UrlSlug = createGameDto.Name.Replace("-", "").Replace(" ", "-").ToLower()
        };

        Context.Add(game);

        Context.SaveChanges();

        return CreatedAtAction(
            nameof(GetGame),
            new { id = game.Id },
            null);
    }

    [HttpPut]
    public IActionResult UpdateGame(UpdateGameDto updateGameDto)
    {
        var game = Context.Game.FirstOrDefault(x => x.Id == updateGameDto.Id);

        if (game == null)
        {
            return BadRequest("Game doesn't exist");
        }

        game.Id = updateGameDto.Id;
        game.Name = updateGameDto.Name;
        game.Description = updateGameDto.Description;
        game.ReleaseYear = updateGameDto.ReleaseYear;
        game.Genre = updateGameDto.Genre;
        game.ImageUrl = updateGameDto.ImageUrl;

        Context.SaveChanges();

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteGame(int id)
    {
        var game = Context.Game.FirstOrDefault(x => x.Id == id);

        if (game != null)
        {
            Context.Game.Remove(game);

            Context.SaveChanges();
        }

        return NoContent();
    }
}
