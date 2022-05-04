namespace Highscore.Areas.API.Models;

public class CreateGameDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int ReleaseYear { get; set; }
    public string Genre { get; set; }
    public Uri ImageUrl { get; set; }
}
