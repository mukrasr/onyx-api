namespace Onyx.Api.IntegrationTests;

public class ProductsTests
{
    private readonly string _apiPath = Environment.GetEnvironmentVariable("API_PATH")!;
    private readonly string _jwtToken = Environment.GetEnvironmentVariable("JWT_TOKEN")!;

    [Fact]
    public async Task Test1()
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Bearer", _jwtToken);
        var resp = await client.GetStringAsync($"{_apiPath}/secret");
        Console.WriteLine(resp);
    }
}