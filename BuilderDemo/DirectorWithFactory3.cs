namespace BuilderDemo;

/// <summary>
/// As prevention of using builder that has been used already,
/// I can just inject some builder factory that creates a builder instance
/// for the director instance itself.
/// </summary>
public class DirectorWithFactory3
{
    private readonly IFactory<string, IPageBuilder> _builderFactory;

    public DirectorWithFactory3(IFactory<string, IPageBuilder> builderFactory) => _builderFactory = builderFactory;

    public IPage Build(string discriminator)
    {
        var builder = _builderFactory.Create(discriminator);
        builder.AddHeader();
        builder.AddTitle();
        builder.AddBody();
        builder.AddFooter();
        return builder.GetPage();
    }
}
