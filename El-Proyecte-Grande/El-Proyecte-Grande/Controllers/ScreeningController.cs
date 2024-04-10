using AspCinema.Models;
using El_Proyecte_Grande.Services;
using Microsoft.AspNetCore.Mvc;

namespace El_Proyecte_Grande.Controllers;


[ApiController]
[Route("[controller]")]

public class ScreeningController : ControllerBase
{

    private readonly IScreeningRepository _screeningRepository;


    public ScreeningController(IScreeningRepository screeningRepository)
    {
        _screeningRepository = screeningRepository;
    }

    [HttpGet]
    public IActionResult GetAllScreenings()
    {
        try
        {
            return Ok(_screeningRepository.GetScreenings());
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("{screeningId}")]
    public IActionResult GetScreeningById(int screeningId)
    {
        try
        {
            Screening screening = _screeningRepository.OneScreening(screeningId);

            if (screening == null)
            {
                return NotFound("Screening not found");
            }

            return Ok(screening);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPost]
    public IActionResult PostScreening(Screening screening)
    {
        try
        {
            return Ok(_screeningRepository.PostScreening(screening));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpDelete("{screeningId}")]
    public IActionResult DeleteScreening([FromRoute]int screeningId)
    {
        try
        {
            return Ok(_screeningRepository.DeleteScreening(screeningId));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpPatch("{screeningID}")]
    public IActionResult UpdateScreening(int screeningID, [FromBody]Screening screening)
    {
        try
        {
            return Ok(_screeningRepository.UpdateScreening(screeningID, screening));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

}