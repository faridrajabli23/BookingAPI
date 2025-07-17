using BookingAPI.Domain.Entities;

namespace BookingAPI.Application.Interfaces;

public interface IHomeService
{
    Task<IEnumerable<Home>> GetAvailableHomesAsync(DateOnly start, DateOnly end);
}

