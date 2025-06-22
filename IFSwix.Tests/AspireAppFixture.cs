using Aspire.Hosting;

public class AspireAppFixture : IAsyncLifetime
{
    public DistributedApplication App { get; private set; } = null!;
    public TimeSpan DefaultTimeout => TimeSpan.FromSeconds(30);
    private IDistributedApplicationBuilder _builder = null!;
    private CancellationToken _cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(30)).Token;

    public async Task InitializeAsync()
    {
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.IFSwix_AppHost>(_cancellationToken);
        _builder = appHost;

        App = await appHost.BuildAsync(_cancellationToken).WaitAsync(DefaultTimeout, _cancellationToken);
        await App.StartAsync(_cancellationToken).WaitAsync(DefaultTimeout, _cancellationToken);
    }

    public async Task DisposeAsync()
    {
        if (App is IAsyncDisposable asyncDisposable)
        {
            await asyncDisposable.DisposeAsync();
        }
    }

    public HttpClient CreateClient(string serviceName)
    {
        return App.CreateHttpClient(serviceName);
    }

    public Task WaitForHealthyAsync(string serviceName)
    {
        return App.ResourceNotifications.WaitForResourceHealthyAsync(serviceName, _cancellationToken);
    }
}
