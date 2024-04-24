using System.Runtime.InteropServices.JavaScript;
using AspCinema.Models;
using El_Proyecte_Grande.Data;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Services;

public class ScreeningRepository : IScreeningRepository
{
    private readonly ElProyecteGrandeContext _movieDbContext;

    public ScreeningRepository(ElProyecteGrandeContext context)
    {
        _movieDbContext = context;
    }

    public IList<Screening> GetAll()
    {
        return _movieDbContext.Screenings.ToList();
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
    
    public async Task SeedScreenings()
    {
        if (!_movieDbContext.Screenings.Any())
        {
            // Get existing Movies and Auditoriums
            var movies = await _movieDbContext.Movies.ToListAsync();
            var auditoriums = await _movieDbContext.Auditoriums.ToListAsync();

            // Create screening data with references
            var screenings = new List<Screening>();

            Random random = new Random();

            DateTime startDate = DateTime.Now.AddDays(1);
            
            foreach (var movie in movies)
            {
                foreach (var auditorium in auditoriums)
                {
                    screenings.Add(new Screening
                    {
                        Movie = movie,
                        Auditorium = auditoriums[random.Next(0, 4)],
                        Start = GetDateTime(screenings) // Example: Random start date within next week
                    });
                }
            }

            // Add screenings to the context
            await _movieDbContext.AddRangeAsync(screenings);
            await _movieDbContext.SaveChangesAsync();
        }
    }

    private DateTime GetDateTime(List<Screening> screenings)
    {
        DateTime date = DateTime.Now.AddDays(1);
        
        if (screenings.Count == 0)
        {
            return new DateTime(date.Year, date.Month, date.Day, 10, 00, 00);
        }

        Screening last = screenings.LastOrDefault();

        DateTime lastStart = last.Start;

        if (last.Start.Hour >= 22)
        {
            return new DateTime(lastStart.Year, lastStart.Month, lastStart.Day + 1, 10, 00, 00);
        }
        
        return new DateTime(lastStart.Year, lastStart.Month, lastStart.Day, lastStart.Hour + 3, 00, 00);
    }
    
    //10:00 - 22:00
    //Min. 3 hours between screenings
    //Round hours
    
    
}