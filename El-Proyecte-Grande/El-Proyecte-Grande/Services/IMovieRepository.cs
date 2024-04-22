using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public interface IMovieRepository
{
    IList<Movie> GetAll();
    Movie GetById(int id);
    void AddMovie(Movie movie);
    void DeleteById(int id);
    void UpdateMovie(Movie movie);
}