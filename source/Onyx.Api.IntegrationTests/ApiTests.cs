using System.Net;
using System.Net.Http.Headers;
using FluentAssertions;

namespace Onyx.Api.IntegrationTests;

public class ApiTests
{
    private readonly string _apiPath = Environment.GetEnvironmentVariable("API_PATH")!;
    private readonly string _jwtToken = Environment.GetEnvironmentVariable("JWT_TOKEN")!;

    [Fact]
    public async Task HealthCheckEndpoint_ShouldReturn200()
    {
        // Arrange
        var client = new HttpClient();

        // Act
        var resp = await client.GetAsync(_apiPath);

        // Assert
        resp.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task SecretEndpoint_ShouldReturn200()
    {
        // Arrange
        var client = new HttpClient();

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);

        // Act
        var resp = await client.GetAsync($"{_apiPath}/secret");

        // Assert
        resp.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}