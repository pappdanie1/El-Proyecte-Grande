using El_Proyecte_Grande.Models;

namespace AspCinema.Models;

public class Seat
{
    public int Id { get; set; }
    public ICollection<SeatReserved> ReservedSeats { get; set; } = new List<SeatReserved>();
    public int Row { get; set; }
    public int Number { get; set; }
    public Auditorium Auditorium { get; set; }
}