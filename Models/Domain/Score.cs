namespace Highscore.Models.Domain;

public class Score
{
    public int Id { get; set; }
    public string PlayerName { get; set; }
    public DateTime Date { get; set; }
    public int PlayerScore { get; set; }
    public Game Game { get; set; }
}

