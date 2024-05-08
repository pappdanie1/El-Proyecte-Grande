using AspCinema.Models;

namespace El_Proyecte_Grande.Models;

public class Screening
{
    public int Id { get; set; }
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    public ICollection<SeatReserved> ReservedSeats { get; set; } = new List<SeatReserved>();
    public DateTime Start { get; set; }
    public Auditorium Auditorium { get; set; }
    public Movie Movie { get; set; }
}