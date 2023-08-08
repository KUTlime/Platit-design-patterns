namespace BuilderDemo;

/// <summary>
/// As prevention of using builder that has been used already,
/// I can just inject some builder factory that creates a builder instance
/// for the director instance itself.
/// </summary>
public class DirectorWithFactory
{
    private readonly IPageBuilder _builder;

    public DirectorWithFactory(IFactory<string, IPageBuilder> builderFactory, string discriminator)
    {
        _builder = builderFactory.Create(discriminator);
        Build();
    }

    public IPage GetPage() => _builder.GetPage();

    private void Build()
    {
        _builder.AddHeader();
        _builder.AddTitle();
        _builder.AddBody();
        _builder.AddFooter();
    }
}
