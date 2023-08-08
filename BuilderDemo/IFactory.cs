namespace BuilderDemo;

public interface IFactory<in TId, out TOut>
{
    TOut Create(TId id);
}
