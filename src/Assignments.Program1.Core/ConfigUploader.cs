namespace Assignments.Program1.Core;

public class ConfigUploader
{
    private readonly IConfigProviderClient _client;

    public ConfigUploader(IConfigProviderClient client) => _client = client;

    public async Task<bool> Upload(IEnumerable<ConfigKey> keys) =>
        await keys.Select(async k => await _client.AddConfigurationAsync(new Request(k.Key, k.Value, false))).First();
}
