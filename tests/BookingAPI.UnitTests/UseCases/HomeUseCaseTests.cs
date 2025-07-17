using BookingAPI.Application.UseCases;
using BookingAPI.Application.Interfaces;
using BookingAPI.Domain.Entities;
using FluentAssertions;
using Moq;

namespace BookingAPI.UnitTests.UseCases;

public class HomeUseCaseTests
{
    [Fact]
    public async Task Should_Return_Homes_That_Match_Date_Range()
    {
        // Arrange
        var homes = new List<Home>
        {
            new Home
            {
                HomeId = "123",
                HomeName = "Home 1",
                AvailableSlots = new() { new(2025, 7, 15), new(2025, 7, 16), new(2025, 7, 17) }
            },
            new Home
            {
                HomeId = "456",
                HomeName = "Home 2",
                AvailableSlots = new() { new(2025, 7, 15), new(2025, 7, 17) }
            }
        };

        var mockService = new Mock<IHomeService>();
        mockService.Setup(x => x.GetAvailableHomesAsync(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                   .ReturnsAsync(homes);

        var useCase = new HomeUseCase(mockService.Object);

        // Act
        var result = await useCase.ExecuteAsync(new(2025, 7, 15), new(2025, 7, 16));

        // Assert
        result.Should().HaveCount(2);
        result.First().HomeId.Should().Be("123");
    }
}
