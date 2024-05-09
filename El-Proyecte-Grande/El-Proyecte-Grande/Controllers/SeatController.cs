using El_Proyecte_Grande.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace El_Proyecte_Grande.Controllers;

[ApiController]
[Route("[controller]"),Authorize(Roles = "User, Admin")]
public class SeatController : ControllerBase
{
    private readonly ISeatRepository _seatRepository;

    public SeatController(ISeatRepository seatRepository)
    {
        _seatRepository = seatRepository;
    }

    [HttpGet("ReservedByScreening")]
    public ActionResult GetByScreening(int screeningId)
    {
        try
        {
            return Ok(_seatRepository.GetReservedByScreening(screeningId));
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}