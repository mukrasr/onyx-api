using FluentAssertions;
using Microsoft.Extensions.Options;
using Onyx.Api.Configs;
using Onyx.Api.Dtos;
using Onyx.Api.Services;

namespace Onyx.Api.UnitTests;

public class ProductsServiceTests
{
    private readonly IEnumerable<Product> _products;
    private readonly ProductsService _service;

    public ProductsServiceTests()
    {
        _products = [
            new (1, "Black t-shirt", Colours.Black),
            new (2, "Blue t-shirt", Colours.Blue),
            new (3, "Green t-shirt", Colours.Green)
        ];

        _service = new(Options.Create(new ProductsConfig
        {
            Products = _products.ToArray()
        }));
    }

    [Fact]
    public async Task GetProducts_ShouldReturn_AllProducts()
    {
        // Act
        var actual = await _service.GetProducts();

        // Assert
        actual.Should().BeEquivalentTo(_products);
    }

    [Theory]
    [InlineData(Colours.Black)]
    [InlineData(Colours.Green)]
    [InlineData(Colours.Blue)]
    public async Task GetProducts_ShouldReturn_ProductsWithColour(Colours colour)
    {
        // Act
        var actual = await _service.GetProducts(colour);

        // Assert
        actual.Should().AllSatisfy(x => x.Colour.Should().Be(colour));
    }
}