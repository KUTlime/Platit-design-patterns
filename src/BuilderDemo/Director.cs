namespace BuilderDemo;

public class Director
{
    private readonly IPageBuilder _builder;
    private bool _isBuilt;

    public Director(IPageBuilder builder) => _builder = builder;

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
