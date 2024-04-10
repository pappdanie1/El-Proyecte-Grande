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
}