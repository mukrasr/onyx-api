using Onyx.Api.Dtos;

namespace Onyx.Api.Configs;

public record ProductsConfig
{
    public required Product[] Products { get; init; }
}