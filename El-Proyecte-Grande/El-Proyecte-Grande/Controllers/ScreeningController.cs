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
}