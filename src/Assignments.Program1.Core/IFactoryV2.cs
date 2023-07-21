namespace Assignments.Program1.Core;

public interface IFactoryV2<TOut>
{
    bool TryMatch(string discriminator, out TOut value);
}

public class FactoryV2 : IFactoryV2<IService>
{
    public bool TryMatch(string discriminator, out IService value) => throw new NotImplementedException();
}
