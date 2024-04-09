using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public interface IMovieRepository
{
    IList<Movie> GetAll();
    Movie AddMovie(Movie movie);
    Movie DeleteById(int id);
}