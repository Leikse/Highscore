using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Highscore.Models.ViewModels;

public class RegisterHighscoreViewModel
{
    [Display(Name = "Spelare")]
    public string PlayerName { get; set; }
    
    public List<SelectListItem>? GameItems { get; set; }
    [Display(Name = "Datum")]
    public DateTime Date { get; set; }
    [Display(Name = "Poäng")]
    public int PlayerScore { get; set; }
    [Display(Name = "Spel")]
    public string Game { get; set; }
}

