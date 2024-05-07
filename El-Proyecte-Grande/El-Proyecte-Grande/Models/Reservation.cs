using El_Proyecte_Grande.Data;

namespace AspCinema.Models;

public class Reservation
{
    public int Id { get; set; }
    public int ScreeningId { get; set; }
    public int CustomerId { get; set; }
    public ICollection<SeatReserved> Seats { get; set; } = new List<SeatReserved>();
    public Screening Screening { get; set; }
    public ApplicationUser Customer { get; set; }
}