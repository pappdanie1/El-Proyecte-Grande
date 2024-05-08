using AspCinema.Models;
using El_Proyecte_Grande.Models;

namespace El_Proyecte_Grande.Services.Repositories;

public interface IAuditoriumRepository
{
    Auditorium GetById(int id);
    void AddAuditorium(Auditorium auditorium);
    void DeleteById(int id);
}