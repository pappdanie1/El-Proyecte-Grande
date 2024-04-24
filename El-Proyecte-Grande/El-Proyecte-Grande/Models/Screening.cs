namespace AspCinema.Models;

public class Screening
{
    public int Id { get; set; }
    public Movie Movie { get; set; }
    public Auditorium? Auditorium { get; set; }
    public DateTime Start { get; set; }
}