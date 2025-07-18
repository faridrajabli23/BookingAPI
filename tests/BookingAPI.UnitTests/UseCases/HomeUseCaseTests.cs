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

        var result = await useCase.ExecuteAsync(new(2025, 7, 15), new(2025, 7, 16));

        result.Should().HaveCount(1);
        result.First().HomeId.Should().Be("123");
    }

    [Fact]
    public async Task Should_Return_Empty_If_NoHomesMatch()
    {
        var homes = new List<Home>();
        var mockService = new Mock<IHomeService>();
        mockService.Setup(x => x.GetAvailableHomesAsync(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                   .ReturnsAsync(homes);

        var useCase = new HomeUseCase(mockService.Object);
        var result = await useCase.ExecuteAsync(new(2025, 7, 15), new(2025, 7, 16));

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task Should_Filter_Homes_Based_On_All_Dates()
    {
        var homes = new List<Home>
        {
            new Home
            {
                HomeId = "789",
                HomeName = "Home 3",
                AvailableSlots = new() { new(2025, 7, 15), new(2025, 7, 16) }
            }
        };

        var mockService = new Mock<IHomeService>();
        mockService.Setup(x => x.GetAvailableHomesAsync(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                   .ReturnsAsync(homes);

        var useCase = new HomeUseCase(mockService.Object);
        var result = await useCase.ExecuteAsync(new(2025, 7, 15), new(2025, 7, 16));

        result.Should().ContainSingle();
        result.First().HomeId.Should().Be("789");
    }

    [Fact]
    public async Task Should_Handle_Overlapping_Dates()
    {
        var homes = new List<Home>
        {
            new Home
            {
                HomeId = "999",
                HomeName = "Overlap Home",
                AvailableSlots = new() { new(2025, 7, 15), new(2025, 7, 17) }
            }
        };

        var mockService = new Mock<IHomeService>();
        mockService.Setup(x => x.GetAvailableHomesAsync(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                   .ReturnsAsync(homes);

        var useCase = new HomeUseCase(mockService.Object);
        var result = await useCase.ExecuteAsync(new(2025, 7, 15), new(2025, 7, 16));

        result.Should().BeEmpty();
    }

    [Fact]
    public async Task Should_Throw_Exception_If_Service_Fails()
    {
        var mockService = new Mock<IHomeService>();
        mockService.Setup(x => x.GetAvailableHomesAsync(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()))
                   .ThrowsAsync(new Exception("Service failed"));

        var useCase = new HomeUseCase(mockService.Object);

        Func<Task> act = async () => await useCase.ExecuteAsync(new(2025, 7, 15), new(2025, 7, 16));

        await act.Should().ThrowAsync<Exception>().WithMessage("Service failed");
    }
}
