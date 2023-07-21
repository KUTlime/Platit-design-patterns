namespace Assignments.Program1.Core;

internal interface IFactory<TIn, TOut>
{
    TOut? Create(TIn discriminator);

    TOut CreateOrDefault(TIn discriminator);
}
