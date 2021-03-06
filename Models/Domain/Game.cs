namespace Highscore.Models.Domain;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public int ReleaseYear { get; set; }
    public Uri ImageUrl { get; set; }
    public string? UrlSlug { get; set; }
}

