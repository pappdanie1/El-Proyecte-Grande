using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public interface IScreeningRepository
{
    List<Screening> GetScreenings();

    Screening OneScreening(int id);

    Screening PostScreening(Screening screening);

    Screening DeleteScreening(int id);

    Screening UpdateScreening(int screeningId, Screening screening);
}