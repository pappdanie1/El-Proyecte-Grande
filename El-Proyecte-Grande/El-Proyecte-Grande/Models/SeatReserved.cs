namespace AspCinema.Models;

public class SeatReserved
{
    public int Id { get; set; }
    public int SeatId { get; set; }
    public int ReservationId { get; set; }
    public int ScreeningId { get; set; }
    
    public Seat Seat { get; set; }
    public Reservation Reservation { get; set; }
    public Screening Screening { get; set; }
}