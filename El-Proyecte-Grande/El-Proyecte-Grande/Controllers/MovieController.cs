using AspCinema.Models;
using El_Proyecte_Grande.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace El_Proyecte_Grande.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieRepository _movieRepository;

    public MovieController(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_movieRepository.GetAll());
    }

    [HttpPost]
    public IActionResult AddMovie(string title, string director, string cast, string description,
        int durationInSec)
    {
        var casts = cast.Split(",").ToList();
        return Ok(_movieRepository.AddMovie(new Movie(_movieRepository.GetAll().Count + 1, title, director, casts, description, durationInSec)));
    }

    [HttpDelete("{movieId}")]
    public IActionResult DeleteMovie([FromRoute] int movieId)
    {
        var movie = _movieRepository.DeleteById(movieId);
        if (movie == null)
        {
            return NotFound("Movie not found");
        }

        return Ok(movie);
    }
    
    [HttpPatch("{movieId}")]
    public IActionResult UpdateMovie([FromRoute] int movieId, [FromBody] Movie updatedMovie)
    {
        var existingMovie = _movieRepository.GetAll().FirstOrDefault(m => m.Id == movieId);
        if (existingMovie == null)
        {
            return NotFound("Movie not found");
        }

        var updated = _movieRepository.UpdateMovie(movieId, updatedMovie);
        if (updated == null)
        {
            return BadRequest("Failed to update movie");
        }

        return Ok(updated);
    }
}