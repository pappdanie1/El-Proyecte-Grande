using AspCinema.Models;
using El_Proyecte_Grande.Data;

namespace El_Proyecte_Grande.Services;

public interface IScreeningRepository
{
    IList<Screening> GetAll();
    Screening? GetById(int id);
    void AddScreening(Screening movie);
    void DeleteById(int id);
    void UpdateScreening(Screening movie);

    Task<List<Screening>> SeedScreenings();
}