using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using Onyx.Api.IntegrationTests.Dtos;

namespace Onyx.Api.IntegrationTests;

public class ProductsControllerTests
{
    private readonly string _apiPath = Environment.GetEnvironmentVariable("API_PATH")!;
    private readonly string _jwtToken = Environment.GetEnvironmentVariable("JWT_TOKEN")!;

    [Fact]
    public async Task GetProducts_ShouldReturnAllProducts()
    {
        // Arrange
        var client = new HttpClient();
        var path = $"{_apiPath}/api/products";

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);

        // Act
        var resp = await client.GetFromJsonAsync<IEnumerable<Product>>(path);

        // Assert
        resp.Should().BeEquivalentTo(new Product[]
        {
            new(1, "Black shirt", Colours.Black),
            new(2, "Blue shirt", Colours.Blue),
            new(3, "Green shirt", Colours.Green)
        });
    }

    [Theory]
    [InlineData(Colours.Black)]
    [InlineData(Colours.Green)]
    [InlineData(Colours.Blue)]
    public async Task GetProducts_ShouldReturn_ProductsWithColour(Colours colour)
    {
        // Arrange
        var client = new HttpClient();
        var path = $"{_apiPath}/api/products/bycolours/{colour}";

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);

        // Act
        var resp = await client.GetFromJsonAsync<IEnumerable<Product>>(path);

        // Assert
        resp.Should().AllSatisfy(x => x.Colour.Should().Be(colour));
    }

    [Fact]
    public async Task GetProducts_ShouldReturn400_IfInvalidColourIsSent()
    {
        // Arrange
        var client = new HttpClient();
        var colour = "INVALID_COLOUR";
        var path = $"{_apiPath}/api/products/bycolours/{colour}";

        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _jwtToken);

        // Act
        var resp = await client.GetAsync(path);

        // Assert
        resp.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Theory]
    [InlineData("api/products")]
    [InlineData("api/products/bycolours/Black")]
    public async Task GetProducts_ShouldReturn401_NoAuthenticationIsProvided(string subPath)
    {
        // Arrange
        var client = new HttpClient();
        var path = $"{_apiPath}/{subPath}";

        // Act
        var resp = await client.GetAsync(path);

        // Assert
        resp.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}