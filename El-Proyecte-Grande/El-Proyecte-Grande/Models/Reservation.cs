using El_Proyecte_Grande.Data;
using El_Proyecte_Grande.Models;

namespace AspCinema.Models;

public class Reservation
{
    public int Id { get; set; }
    public ICollection<SeatReserved> ReservedSeats { get; set; } = new List<SeatReserved>();
    public Screening Screening { get; set; }
    public ApplicationUser Customer { get; set; }
}