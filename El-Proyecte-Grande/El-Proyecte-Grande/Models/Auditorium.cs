namespace AspCinema.Models;

public class Auditorium
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SeatNo { get; set; }
    
    public Auditorium(string name, int seatNo)
    {
        Name = name;
        SeatNo = seatNo;
    }
}