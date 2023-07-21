namespace Assignments.Program1.Core;

public interface IConfigProviderClient
{
    public Task<bool> AddConfigurationAsync(IRequest request);
}
