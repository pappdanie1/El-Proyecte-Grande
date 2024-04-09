namespace AspCinema.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public IEnumerable<string> Cast { get; set; }
    public string Description { get; set; }
    public int DurationInSec { get; set; }
    
    public Movie(int id, string title, string director, IEnumerable<string> cast, string description, int durationInSec)
    {
        Id = id;
        Title = title;
        Director = director;
        Cast = cast;
        Description = description;
        DurationInSec = durationInSec;
    }
}