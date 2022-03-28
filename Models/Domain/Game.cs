namespace Highscore.Models.Domain;

public class Game
{
    public int Id { get; protected set; }
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public string Genre { get; protected set; }
    public int ReleaseYear { get; protected set; }
    public Uri ImageUrl { get; protected set; }
    public string UrlSlug { get; protected set; }
}

