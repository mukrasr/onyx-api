using Onyx.Api.Dtos;

namespace Onyx.Api.Services;

public interface IProductsService
{
    Task<IEnumerable<Product>> GetProducts();
    Task<IEnumerable<Product>> GetProducts(Colours colour);
}