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
    
        //for 7 days
    /*
    public async Task<List<Screening>> SeedScreenings()
    {
        var screenings = new List<Screening>();

        if (!_movieDbContext.Screenings.Any())
        {
            var movies = await _movieDbContext.Movies.ToListAsync();
            var auditoriums = await _movieDbContext.Auditoriums.ToListAsync();

            // Generate screenings for the next 7 days
            for (int dayOffset = 1; dayOffset <= 7; dayOffset++)
            {
                var tomorrow = DateTime.Today.AddDays(dayOffset);

                var startTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 10, 0, 0);
                var endTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 22, 0, 0);

                //var interval = TimeSpan.FromHours(3);

                var currentAuditoriumIndex = 0;

                foreach (var movie in movies)
                {
                    var currentAuditorium = auditoriums[currentAuditoriumIndex];

                    var currentTime = startTime;

                    while (currentTime < endTime)
                    {
                        screenings.Add(new Screening
                        {
                            Movie = movie,
                            Auditorium = currentAuditorium,
                            Start = currentTime
                        });

                        currentTime = currentTime.AddHours(random.Next(1, 4) * 3);
                    }

                    currentAuditoriumIndex = (currentAuditoriumIndex + 1) % auditoriums.Count;
                }
            }
        }

        return screenings;
    }
    */

    //for 1 day

    public async Task<List<Screening>> SeedScreenings()
    {
        var screenings = new List<Screening>();

        if (!_movieDbContext.Screenings.Any())
        {
            var movies = await _movieDbContext.Movies.ToListAsync();
            var auditoriums = await _movieDbContext.Auditoriums.ToListAsync();
            
            var tomorrow = DateTime.Today.AddDays(1);
            
            var startTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 10, 0, 0);
            var endTime = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 22, 0, 0);
            
            //var interval = TimeSpan.FromHours(3);
            
            var currentAuditoriumIndex = 0;

            foreach (var movie in movies)
            {
                var currentAuditorium = auditoriums[currentAuditoriumIndex];

                var currentTime = startTime;

                while (currentTime < endTime)
                {
                    // Create a new screening
                    screenings.Add(new Screening
                    {
                        Movie = movie,
                        Auditorium = currentAuditorium,
                        Start = currentTime
                    });
                    
                    currentTime = currentTime.AddHours(random.Next(1, 4) * 3);
                }
                
                currentAuditoriumIndex = (currentAuditoriumIndex + 1) % auditoriums.Count;
            }
        }

        return screenings;
    }
    

   
}