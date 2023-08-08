namespace BuilderDemo;

/// <summary>
/// As prevention of using builder that has been used already,
/// I can just inject some builder factory that creates a builder instance
/// for the director instance itself.
/// </summary>
public class DirectorWithFactory2
{
    private readonly IFactory<string, IPageBuilder> _builderFactory;
    private IPage? _page;

    public DirectorWithFactory2(IFactory<string, IPageBuilder> builderFactory) => _builderFactory = builderFactory;

    public IPage GetPage() => _page ?? throw new InvalidOperationException($"Please, call {nameof(Build)} method first.");

    public void Build(string discriminator)
    {
        var builder = _builderFactory.Create(discriminator);
        builder.AddHeader();
        builder.AddTitle();
        builder.AddBody();
        builder.AddFooter();
        _page = builder.GetPage();
    }
}
