using AspCinema.Models;

namespace El_Proyecte_Grande.Services;

public interface IMovieRepository
{
    IList<Movie> GetAll();
    Movie GetById(int id);
    Movie AddMovie(Movie movie);
    Movie DeleteById(int id);
    Movie UpdateMovie(int id, Movie updatedMovie);
}