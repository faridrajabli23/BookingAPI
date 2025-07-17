namespace BookingAPI.Domain.Entities;

public class Home
{
    public string HomeId { get; set; } = string.Empty;
    public string HomeName { get; set; } = string.Empty;
    public List<DateOnly> AvailableSlots { get; set; } = new();
}
