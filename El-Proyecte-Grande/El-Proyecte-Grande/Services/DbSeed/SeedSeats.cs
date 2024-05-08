using AspCinema.Models;
using El_Proyecte_Grande.Data;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Services.DbSeed;

public class SeedSeats : ISeedSeats
{
    private readonly AspCinemaContext _context;

    public SeedSeats(AspCinemaContext context)
    {
        _context = context;
    }

    public async Task<List<Seat>> Seed()
    {
        var seats = new List<Seat>();
        if (!_context.Seats.Any())
        {
            var auditoriums = await _context.Auditoriums.ToListAsync();
            foreach (var auditorium in auditoriums)
            {
                var rows = 5;
                var seatsInRow = 6;
                for (int j = 1; j <= rows; j++)
                {
                    for (int k = 1; k <= seatsInRow; k++)
                    {
                        var seat = new Seat
                        {
                            Auditorium = auditorium,
                            Number = k,
                            Row = j
                        };
                        seats.Add(seat);
                        auditorium.Seats.Add(seat);
                    }
                } 
            }
        }

        return seats;
    }
}