using AspCinema.Models;

namespace El_Proyecte_Grande.Models;

public class Auditorium
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Seat> Seats { get; set; } = new List<Seat>();
    public ICollection<Screening> Screenings { get; set; } = new List<Screening>();
}