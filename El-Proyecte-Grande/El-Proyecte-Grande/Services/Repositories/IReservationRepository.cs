using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public interface IReservationRepository
{
    IList<Reservation> GetAllByUser(string userName);
    IList<Reservation> GetAllByScreening(int screeningId);
    Reservation? GetById(int id, string userName);
    void AddReservation(Reservation reservation);
    void DeleteById(int id);
}