using AspCinema.Models;
using El_Proyecte_Grande.Data;
using Microsoft.EntityFrameworkCore;

namespace El_Proyecte_Grande.Services;

public class ReservationRepository : IReservationRepository
{
    private readonly AspCinemaContext _aspCinemaContext;
    private readonly AspCinemaContext _elProyecteGrandeContext;

    public ReservationRepository(AspCinemaContext elProyecteGrandeContext, AspCinemaContext aspCinemaContext)
    {
        _elProyecteGrandeContext = elProyecteGrandeContext;
        _aspCinemaContext = aspCinemaContext;
    }
    
    //All reservations for a given user
    public IList<Reservation> GetAllByUser(string userName)
    {
        return _elProyecteGrandeContext.Reservations.Where(r => r.Customer.UserName == userName).ToList();
    }

    //All reservations for a given screening
    public IList<Reservation> GetAllByScreening(int screeningId)
    {
        return _elProyecteGrandeContext.Reservations.Where(r => r.Screening.Id == screeningId).ToList();
    }

    //for deleting a reservation for a user
    public Reservation? GetById(int id, string userName)
    {
        return _elProyecteGrandeContext.Reservations.Include(r => r.Customer)
            .FirstOrDefault(r => r.Id == id && r.Customer.UserName == userName);
    }

    public void AddReservation(Reservation reservation)
    {
        _elProyecteGrandeContext.Add(reservation);
        _elProyecteGrandeContext.SaveChanges();
    }

    public void DeleteById(int id)
    {
        _elProyecteGrandeContext.Reservations.Remove(_elProyecteGrandeContext.Reservations.FirstOrDefault(r => r.Id == id));
        _elProyecteGrandeContext.SaveChanges();
    }
}