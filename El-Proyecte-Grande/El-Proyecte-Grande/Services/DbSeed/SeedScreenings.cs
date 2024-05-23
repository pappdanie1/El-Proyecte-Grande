using El_Proyecte_Grande.Data;
using El_Proyecte_Grande.Models;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Services.DbSeed;

public class SeedScreenings : ISeedScreenings
{

    private readonly AspCinemaContext _movieDbContext;

    public SeedScreenings(AspCinemaContext context)
    {
        _movieDbContext = context;
    }

    public async Task<List<Screening>> Seed()
    {
        var screenings = new List<Screening>();
        Random random = new Random();
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
   
}