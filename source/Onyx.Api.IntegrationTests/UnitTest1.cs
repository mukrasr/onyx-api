namespace Onyx.Api.IntegrationTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var apiPath = Environment.GetEnvironmentVariable("API_PATH");
        Console.WriteLine($"API_PATH={apiPath}");
    }
}