namespace Assignments.Program1.Core.Unit.Tests;

internal class TestConfigProviderClient : IConfigProviderClient
{
    private readonly Func<IRequest, bool> _func;

    public TestConfigProviderClient(Func<IRequest, bool> assertion) => _func = assertion;

    public bool AssertionResult { get; private set; }

    public Task<bool> AddConfigurationAsync(IRequest request)
    {
        AssertionResult = _func(request);
        return Task.FromResult(AssertionResult);
    }
}
