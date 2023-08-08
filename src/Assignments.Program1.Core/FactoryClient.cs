namespace Assignments.Program1.Core;

internal class FactoryClient : ICarFactory
{
    public ICar? Create(string discriminator) => throw new NotImplementedException();

    public ICar CreateOrDefault(string discriminator) => throw new NotImplementedException();

#pragma warning disable IDE0051
    private void DoSomething()
#pragma warning restore IDE0051
    {
        var factory = new FactoryWithDiscriminatedUnion();
        var result = factory.CreateOrDefault("asdfasdf");
        var output = result.Match<IService>(
            publicService => publicService,
            privateService => privateService,
            _ => new PublicService());
    }
}
