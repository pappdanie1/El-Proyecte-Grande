using El_Proyecte_Grande.Models;

namespace AspCinema.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Director { get; set; }
    public string? Cast { get; set; }
    public string? Description { get; set; }
    public string? DurationInSec { get; set; }
    public string? Poster { get; set; }
    public string? Genres { get; set; }
    public ICollection<Screening>? Screenings { get; set; } = new List<Screening>();
}