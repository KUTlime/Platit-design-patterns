namespace Assignments.Program1.Core;

public interface IFactoryV1<TOut>
{
    TOut CreateOrDefault(string discriminator);
}
