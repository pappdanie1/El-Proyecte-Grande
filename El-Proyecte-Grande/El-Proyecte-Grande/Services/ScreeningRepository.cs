using System.Runtime.InteropServices.JavaScript;
using AspCinema.Models;
using El_Proyecte_Grande.Data;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Services;

public class ScreeningRepository : IScreeningRepository
{
    private readonly ElProyecteGrandeContext _movieDbContext;
    Random random = new Random();

    public ScreeningRepository(ElProyecteGrandeContext context)
    {
        _movieDbContext = context;
    }

    public IList<Screening> GetAll()
    {
        return _movieDbContext.Screenings
            .Include(s => s.Movie)
            .Include(s => s.Auditorium)
            .ToList();
    }

    public Screening? GetById(int id)
    {
        return _movieDbContext.Screenings.FirstOrDefault(s => s.Id == id);
    }

    public void AddScreening(Screening screening)
    {
        _movieDbContext.Add(screening);
        _movieDbContext.SaveChanges();
    }

    public void DeleteById(int id)
    {
        
        _movieDbContext.Remove(_movieDbContext.Screenings.FirstOrDefault(s => s.Id == id));
        _movieDbContext.SaveChanges();
    }
    
    public void UpdateScreening(Screening screening)
    {
        _movieDbContext.Update(screening);
        _movieDbContext.SaveChanges();
    }
    
    
    public async Task<List<Screening>> SeedScreenings()
    {
        var screenings = new List<Screening>();
        if (!_movieDbContext.Screenings.Any())
        {
            var movies = await _movieDbContext.Movies.ToListAsync();
            var auditoriums = await _movieDbContext.Auditoriums.ToListAsync();
            
            foreach (var movie in movies)
            {
                for (int i = 0; i < 2; i++)
                {
                    var auditoriumIndex = random.Next(0, 2);
                    
                    screenings.Add(new Screening
                    {
                        Movie = movie,
                        Auditorium = auditoriums[auditoriumIndex],
                        Start = GetDateTime(screenings, auditoriumIndex, movie) 
                    });
                }
            }
            
        }
        return screenings;
    }

    private DateTime GetDateTime(List<Screening> screenings, int auditoriumIndex, Movie movie)
    {
        DateTime date = DateTime.Now.AddDays(1);
        
        if (screenings.Count == 0)
        {
            return new DateTime(date.Year, date.Month, date.Day, 10, 00, 00);
        }

        var isLastSameScreening = screenings.Exists(s => s.Movie == movie);
        var lastSameScreening = screenings.LastOrDefault(s => s.Movie == movie);
        var isLastSameAuditorium = screenings.Exists(s => s.Auditorium.Id == auditoriumIndex);
        var lastSameAuditorium = screenings.LastOrDefault(s => s.Auditorium.Id == auditoriumIndex);
        var lastScreening = screenings.LastOrDefault();
        DateTime lastStart = lastScreening.Start;


        if (isLastSameScreening)
        {
            if (lastSameScreening.Start.Hour >= 16)
            {
                return new DateTime(lastStart.Year, lastStart.Month, lastStart.Day + 1, 10, 00, 00);
            }
            else
            {
                return new DateTime(lastStart.Year, lastStart.Month, lastStart.Day, lastStart.Hour + 6, 00, 00);
            }
            

        }
        if (lastStart.Hour >= 22)
        {
            return new DateTime(lastStart.Year, lastStart.Month, lastStart.Day + 1, 10, 00, 00);
        }
        
        return new DateTime(lastStart.Year, lastStart.Month, lastStart.Day, lastStart.Hour + 3, 00, 00);
    }
    
    //10:00 - 22:00
    //Min. 3 hours between screenings
    //Round hours
    
    
   
}