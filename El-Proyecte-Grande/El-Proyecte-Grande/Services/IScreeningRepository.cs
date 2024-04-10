using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public interface IScreeningRepository
{
    List<Screening> GetScreenings();
}