namespace AspCinema.Models;

public class Seat_reserved : Seat
{
    public Reservation Reservation { get; set; }
    public Screening Screening { get; set; }

    public Seat_reserved(int row, int number, Auditorium auditorium, Reservation reservation, Screening screening) : base(row, number, auditorium)
    {
        Reservation = reservation;
        Screening = screening;
    }
}