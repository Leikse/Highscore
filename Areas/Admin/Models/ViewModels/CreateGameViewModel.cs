using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Highscore.Models.ViewModels;

public class CreateGameViewModel
{
    [Display(Name = "Titel")]
    public string Name { get; set; }
    [Display(Name = "Beskrivning")]
    public string Description { get; set; }
    [Display(Name = "Bild URL")]
    public Uri ImageUrl { get; set; }
    [Display(Name = "Genre")]
    public string Genre { get; set; }
    [Display(Name = "År")]
    public int ReleaseYear { get; set; }
    public List<SelectListItem>? GameItems { get; set; }
}

