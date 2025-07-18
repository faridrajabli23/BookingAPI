using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;

namespace BookingAPI.IntegrationTests.Api;

public class AvailableHomesApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AvailableHomesApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsAvailableHomesWithStatus200()
    {
        var url = "/api/available-homes?startDate=2025-07-15&endDate=2025-07-16";
        var response = await _client.GetAsync(url);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain("Home 1");
    }

    [Fact]
    public async Task ReturnsEmpty_When_NoHomesAvailable()
    {
        var url = "/api/available-homes?startDate=2099-01-01&endDate=2099-01-05";
        var response = await _client.GetAsync(url);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain("\"homes\":[]");
    }

    [Fact]
    public async Task ReturnsHome_WhenOnlyOneDateIsRequested()
    {
        var url = "/api/available-homes?startDate=2025-07-15&endDate=2025-07-15";
        var response = await _client.GetAsync(url);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var json = await response.Content.ReadAsStringAsync();
        json.Should().Contain("Home 1");
    }

    [Fact]
    public async Task ReturnsOnlyHomesThatFullyMatchDateRange()
    {
        var url = "/api/available-homes?startDate=2025-07-15&endDate=2025-07-16";
        var response = await _client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        json.Should().Contain("Home 1");
        json.Should().NotContain("Home 2");
    }

    [Fact]
    public async Task ReturnsManyHomes_WhenSeedDataIsPopulated()
    {
        var url = $"/api/available-homes?startDate={DateTime.Today:yyyy-MM-dd}&endDate={DateTime.Today.AddDays(1):yyyy-MM-dd}";
        var response = await _client.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        json.Should().Contain("\"homes\":");
        json.Should().NotContain("\"homes\":[]");
    }
}
