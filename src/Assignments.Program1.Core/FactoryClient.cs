namespace Assignments.Program1.Core;

internal class FactoryClient : ICarFactory
{
    public ICar? Create(string discriminator) => throw new NotImplementedException();

    public ICar CreateOrDefault(string discriminator) => throw new NotImplementedException();

    private void DoSomething()
    {
        var factory = new FactoryWithDiscriminatedUnion();
        var result = factory.CreateOrDefault("asdfasdf");
        var output = result.Match<IService>(
            publicService => publicService,
            privateService => privateService,
            _ => new PublicService());
    }
}
