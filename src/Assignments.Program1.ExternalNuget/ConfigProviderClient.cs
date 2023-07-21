namespace Assignments.Program1.ExternalNuget;

public class ConfigProviderClient
{
    private readonly ApiConfiguration _configuration;

    public ConfigProviderClient(ApiConfiguration configuration) => _configuration = configuration;

    public async Task<bool> AddConfigurationAsync(Request request)
    {
        var client = new HttpClient();
        var content = new StringContent($"{request.Key};{request.Value}");
        var response = await client.PostAsync(_configuration.ConfigProviderUri, content);
        return response.StatusCode == System.Net.HttpStatusCode.OK;
    }
}
