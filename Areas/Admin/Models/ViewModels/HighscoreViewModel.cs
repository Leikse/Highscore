using System.ComponentModel.DataAnnotations;
using Highscore.Models.Domain;

namespace Highscore.Areas.Admin.Models.ViewModels;

public class HighscoreViewModel
{
    public int Id { get; set; }
    [Display(Name = "Spelare")]
    public string PlayerName { get; set; }
    public string Date { get; set; }
    public int PlayerScore { get; set; }
    public string Game { get; set; }

    public HighscoreViewModel(int id, string playerName, string date, int playerScore, string game)
    {
        Id = id;
        PlayerName = playerName;
        Date = date;
        PlayerScore = playerScore;
        Game = game;
    }
}