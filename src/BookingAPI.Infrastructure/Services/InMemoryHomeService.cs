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
        _homes.TryAdd("1123", new Home
        {
            HomeId = "1123",
            HomeName = "Home 1",
            AvailableSlots = new() { new(2025, 7, 15), new(2025, 7, 16), new(2025, 7, 17) }
        });

        _homes.TryAdd("1456", new Home
        {
            HomeId = "1456",
            HomeName = "Home 2",
            AvailableSlots = new() { new(2025, 7, 17), new(2025, 7, 18) }
        });

        var homes = GenerateSeedData();
        foreach (var home in homes)
        {
            _homes.TryAdd(home.HomeId, home);
        }
    }

    private static List<Home> GenerateSeedData()
    {
        var homes = new List<Home>();
        var random = new Random();

        for (int i = 1; i <= 1000; i++)
        {
            var availableSlots = new List<DateOnly>();

            var today = DateTime.Today;
            for (int j = 0; j < 10; j++)
            {
                var date = today.AddDays(random.Next(0, 30));
                var dateOnly = DateOnly.FromDateTime(date);
                if (!availableSlots.Contains(dateOnly))
                    availableSlots.Add(dateOnly);
            }

            homes.Add(new Home
            {
                HomeId = i.ToString(),
                HomeName = $"Home {i}",
                AvailableSlots = availableSlots.OrderBy(d => d).ToList()
            });
        }

        return homes;
    }
}
