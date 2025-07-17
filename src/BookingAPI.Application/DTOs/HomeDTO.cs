namespace BookingAPI.Application.DTOs;

public class HomeDTO
{
    public string HomeId { get; set; } = string.Empty;
    public string HomeName { get; set; } = string.Empty;
    public List<string> AvailableSlots { get; set; } = new();
}

