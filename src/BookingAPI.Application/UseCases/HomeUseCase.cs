using BookingAPI.Application.DTOs;
using BookingAPI.Application.Interfaces;

namespace BookingAPI.Application.UseCases;

public class HomeUseCase
{
    private readonly IHomeService _homeService;

    public HomeUseCase(IHomeService homeService)
    {
        _homeService = homeService;
    }

    public async Task<List<HomeDTO>> ExecuteAsync(DateOnly start, DateOnly end)
    {
        var homes = await _homeService.GetAvailableHomesAsync(start, end);

        return homes.Select(h => new HomeDTO
        {
            HomeId = h.HomeId,
            HomeName = h.HomeName,
            AvailableSlots = h.AvailableSlots
                .Where(d => d >= start && d <= end)
                .Select(d => d.ToString("yyyy-MM-dd"))
                .ToList()
        }).ToList();
    }
}
