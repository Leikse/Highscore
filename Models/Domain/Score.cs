using System.ComponentModel.DataAnnotations.Schema;

namespace Highscore.Models.Domain;

public class Score
{
    public int Id { get; protected set; }
    [ForeignKey("GameId")]
    public int GameId { get; protected set; }
    public string GameName { get; protected set; }
    public string PlayerName { get; protected set; }
    public DateTime Date { get; protected set; }
    public int PlayerScore { get; protected set; }
    public string UrlSlug { get; protected set; }
}

