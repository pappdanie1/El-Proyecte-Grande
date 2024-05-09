using AspCinema.Models;

namespace El_Proyecte_Grande.Models;

public class SeatReserved
{
    public int Id { get; set; }
    public Seat Seat { get; set; }
    public Reservation Reservation { get; set; }
    public Screening Screening { get; set; }
}