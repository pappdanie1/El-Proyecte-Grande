namespace AspCinema.Models;

public class Screening
{
    public int Id { get; set; }
    public Movie Movie { get; set; }
    public Auditorium? Auditorium { get; set; }
    public DateTime Start { get; set; }
    
    public Screening(int id, Movie movie, Auditorium? auditorium, DateTime start)
    {
        Id = id;
        Movie = movie;
        Auditorium = auditorium;
        Start = start;
    }
}