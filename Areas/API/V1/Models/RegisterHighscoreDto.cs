using Highscore.Models.Domain;

namespace Highscore.Areas.API.V1.Models;

public class RegisterHighscoreDto
{
    public string PlayerName { get; set; }
    public string Date { get; set; }
    public int Score { get; set; }
    public string Game { get; set; }
}
