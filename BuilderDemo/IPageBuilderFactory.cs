namespace BuilderDemo;

public interface IPageBuilderFactory
{
    static abstract IPageBuilder Create(string discriminator);
}
