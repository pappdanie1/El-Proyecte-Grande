using El_Proyecte_Grande.Models;

namespace El_Proyecte_Grande.Services.Repositories;

public interface IScreeningRepository
{
    IList<Screening> GetAll();
    Screening? GetById(int id);
    void AddScreening(Screening movie);
    void DeleteById(int id);
    void UpdateScreening(Screening movie);
}