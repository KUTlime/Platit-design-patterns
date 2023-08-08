namespace BuilderDemo;

/// <summary>
/// As prevention of using builder that has been used already,
/// I can just inject some builder factory that creates a builder instance
/// for the director instance itself.
/// </summary>
public class DirectorWithFactory
{
    private readonly IPageBuilder _builder;
    private bool _isBuilt;

    public DirectorWithFactory(IPageBuilder builder) => _builder = builder;

    public void Build()
    {
        _builder.AddHeader();
        _builder.AddTitle();
        _builder.AddBody();
        _builder.AddFooter();
        _isBuilt = true;
    }

    public IPage GetPage() =>
        _isBuilt
            ? _builder.GetPage()
            : throw new InvalidOperationException($"Please, call {nameof(Build)} method first.");
}
