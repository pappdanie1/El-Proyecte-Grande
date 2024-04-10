using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public class ScreeningRepository : IScreeningRepository
{
    private List<Screening> Screenings { get; set; }
    private readonly IMovieRepository _movieRepository;
    
    public ScreeningRepository(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
        Screenings = new List<Screening>()
        {
            new Screening(1, _movieRepository.GetAll()[0], null, DateTime.Now),
            new Screening(2, _movieRepository.GetAll()[1], null, DateTime.Now),
            new Screening(3, _movieRepository.GetAll()[2], null, DateTime.Now)
        };
        
    }

    public List<Screening> GetScreenings()
    {
        return Screenings;
    }

    public Screening OneScreening(int id)
    {
        Screening screening = Screenings.Find(screen => screen.Id == id);

        if (screening == null)
        {
            return null;
        }

        return screening;
    }

    public Screening PostScreening(Screening screening)
    {
        screening.Id = Screenings.Count + 1;
        Screenings.Add(screening);
        return screening;
    }

    public Screening DeleteScreening(int id)
    {
        Screening screening = Screenings.Find(screen => screen.Id == id);

        if (screening == null)
        {
            return null;
        }
        Screenings.Remove(screening);

        return screening;
    }

    public Screening UpdateScreening(int screeningId, Screening screening)
    {
        var screeningToUpdate = Screenings.Find(screen => screen.Id == screeningId);

        if (screeningToUpdate != null)
        {
            screeningToUpdate.Movie = screening.Movie;
            screeningToUpdate.Auditorium = screening.Auditorium;
            screeningToUpdate.Start = screening.Start;
        }

        return screening;
    }
    
}