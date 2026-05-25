using AndidaTour.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AndidaTour.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FlightsController : ControllerBase
{
    private readonly FlightService _flightService;
    public FlightsController(FlightService flightService) => _flightService = flightService;

    [HttpGet("search")]
    public IActionResult Search([FromQuery] string from, [FromQuery] string to, [FromQuery] string cabin = "economica")
    {
        var results = _flightService.GenerateMockFlights(from, to, cabin);
        return Ok(results);
    }

     [HttpGet("airports")]
    public IActionResult GetAirports() => Ok(_flightService.GetAirports());

    [HttpGet("airlines")]
    public IActionResult GetAirlines() => Ok(_flightService.GetAirlines());
}