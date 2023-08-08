namespace BuilderDemo;

public class DirectorFluentApi
{
    private readonly IPageBuilder _builder;
    private bool _isBuilt;

    public DirectorFluentApi(IPageBuilder builder) => _builder = builder;

    public void Build() => _builder.AddHeader().AddTitle().AddBody().AddFooter();

    public IPage GetPage() =>
        _isBuilt
            ? _builder.GetPage()
            : throw new InvalidOperationException($"Please, call {nameof(Build)} method first.");
}
