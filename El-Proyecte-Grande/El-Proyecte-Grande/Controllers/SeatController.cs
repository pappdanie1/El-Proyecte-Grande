using El_Proyecte_Grande.Services;
using El_Proyecte_Grande.Services.DbSeed;
using El_Proyecte_Grande.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace El_Proyecte_Grande.Controllers;

[ApiController]
[Route("[controller]")]
public class SeatController : ControllerBase
{
    private readonly ISeatRepository _seatRepository;
    private readonly ISeedSeats _seedSeats;

    public SeatController(ISeedSeats seedSeats, ISeatRepository seatRepository)
    {
        _seedSeats = seedSeats;
        _seatRepository = seatRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        try
        {
            var seats = _seatRepository.GetAll();
            if (seats.Count != 0)
            {
                return Ok(seats);
            }
            else
            {
                var seed = await _seedSeats.Seed();
                foreach (var seat in seed)
                {
                    _seatRepository.AddSeat(seat);
                }

                return Ok(seed);
            }
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}