using AspCinema.Models;
using El_Proyecte_Grande.Data;
using El_Proyecte_Grande.Models;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Services.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly AspCinemaContext _context;

    public SeatRepository(AspCinemaContext context)
    {
        _context = context;
    }
    
    public Seat? GetById(int id)
    {
        return _context.Seats.FirstOrDefault(s => s.Id == id);
    }

    public List<SeatReserved> GetReservedByScreening(int screeningId)
    {
        return _context.ReservedSeats.Where(r => r.Screening.Id == screeningId)
            .Include(r => r.Seat).ToList();
    }

    public void AddReservedSeat(SeatReserved reservedSeat)
    {
        _context.ReservedSeats.Add(reservedSeat);
        _context.SaveChanges();
    }
}