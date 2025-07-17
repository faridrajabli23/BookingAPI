using BookingAPI.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace BookingAPI.API.Controllers;

[ApiController]
[Route("api/available-homes")]
public class HomeController : ControllerBase
{
    private readonly HomeUseCase _homeUseCase;

    public HomeController(HomeUseCase homeUseCase)
    {
        _homeUseCase = homeUseCase;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        if (endDate < startDate)
            return BadRequest("End date must be after start date.");

        var homes = await _homeUseCase.ExecuteAsync(startDate, endDate);
        return Ok(new
        {
            status = "OK",
            homes
        });
    }
}
