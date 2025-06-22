namespace IFSwix.Tests.Tests;

public class IntegrationTests : IClassFixture<AspireAppFixture>
{
    private readonly AspireAppFixture _fixture;

    public IntegrationTests(AspireAppFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task VehicleInfoApiRootReturnsOk()
    {
        await _fixture.WaitForHealthyAsync("vehicleInfoApiService");
        var client = _fixture.CreateClient("vehicleInfoApiService");
        var response = await client.GetAsync("/");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task InsuranceApiRootReturnsOk()
    {
        await _fixture.WaitForHealthyAsync("insuranceApiService");
        var client = _fixture.CreateClient("insuranceApiService");
        var response = await client.GetAsync("/");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
