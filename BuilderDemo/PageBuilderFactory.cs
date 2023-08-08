namespace BuilderDemo;

public class PageBuilderFactory : IPageBuilderFactory
{
    // Implementace interface
    public static IPageBuilder Create(string discriminator) => throw new NotImplementedException();
}
