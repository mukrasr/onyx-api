using Microsoft.Extensions.Options;
using Onyx.Api.Configs;
using Onyx.Api.Dtos;

namespace Onyx.Api.Services;

public class ProductsService(IOptions<ProductsConfig> config) : IProductsService
{
    private readonly IEnumerable<Product> _products = config.Value.Products;

    public Task<IEnumerable<Product>> GetProducts()
        => Task.FromResult(_products.AsEnumerable());

    public async Task<IEnumerable<Product>> GetProducts(Colours colour)
        => (await GetProducts()).Where(x => x.Colour == colour);
}
