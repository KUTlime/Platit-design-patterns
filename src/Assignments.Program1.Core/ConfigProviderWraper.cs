namespace Assignments.Program1.Core;

public class ConfigProviderWraper : IConfigProviderClient
{
    private readonly ConfigProviderClient _client;

    public ConfigProviderWraper(ConfigProviderClient client) => _client = client;

    public async Task<bool> AddConfigurationAsync(IRequest request) =>
        await _client.AddConfigurationAsync(new ExternalNuget.Request(request.Key, request.Value, request.Encrypted));
}
