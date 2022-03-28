namespace Highscore.Models.Domain;

public class Score
{
    public int Id { get; protected set; }
    public string PlayerName { get; protected set; }
    public DateTime Date { get; protected set; }
    public int PlayerScore { get; protected set; }
    public Game Game { get; protected set; }
}

