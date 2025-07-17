using BookingAPI.Application.Interfaces;
using BookingAPI.Domain.Entities;
using System.Collections.Concurrent;

namespace BookingAPI.Infrastructure.Services;

public class InMemoryHomeService : IHomeService
{
    private readonly ConcurrentDictionary<string, Home> _homes = new();

    public InMemoryHomeService()
    {
        Seed();
    }

    public Task<IEnumerable<Home>> GetAvailableHomesAsync(DateOnly start, DateOnly end)
    {
        var requiredDates = Enumerable.Range(0, (end.DayNumber - start.DayNumber) + 1)
            .Select(offset => start.AddDays(offset))
            .ToList();

        var result = _homes.Values
            .Where(home => requiredDates.All(date => home.AvailableSlots.Contains(date)));

        return Task.FromResult(result);
    }

    private void Seed()
    {
        _homes.TryAdd("123", new Home
        {
            HomeId = "123",
            HomeName = "Home 1",
            AvailableSlots = new() { new(2025, 7, 15), new(2025, 7, 16), new(2025, 7, 17) }
        });

        _homes.TryAdd("456", new Home
        {
            HomeId = "456",
            HomeName = "Home 2",
            AvailableSlots = new() { new(2025, 7, 17), new(2025, 7, 18) }
        });
    }
}
