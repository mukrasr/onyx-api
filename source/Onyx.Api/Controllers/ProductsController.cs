using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onyx.Api.Services;

namespace Onyx.Api.Dtos;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductsService service) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<Product>> GetProductsAsync()
        => await service.GetProducts();

    [HttpGet]
    [Route("bycolours/{colour}")]
    public async Task<IEnumerable<Product>> GetProductsAsync(Colours colour)
        => await service.GetProducts(colour);
}