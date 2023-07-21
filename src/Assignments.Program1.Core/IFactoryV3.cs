namespace Assignments.Program1.Core;

public interface IFactoryV3<TIn, TCreateOut, TCreateDefaultOut>
{
    TCreateOut Create(TIn discriminator);

    TCreateDefaultOut CreateOrDefault(TIn discriminator);
}

public class FactoryV3 : IFactoryV3<string, IService?, IService>
{
    public IService? Create(string discriminator) => throw new NotImplementedException();

    public IService CreateOrDefault(string discriminator) => throw new NotImplementedException();
}
