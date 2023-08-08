namespace BuilderDemo;

public class DirectorFluentApi
{
    private readonly IPageBuilderFluentApi _builder;
    private bool _isBuilt;

    public DirectorFluentApi(IPageBuilderFluentApi builder) => _builder = builder;

    public void Build()
    {
        _ = _builder.AddHeader().AddTitle().AddBody().AddFooter();
        _isBuilt = true;
    }

    public IPage GetPage() =>
        _isBuilt
            ? _builder.GetPage()
            : throw new InvalidOperationException($"Please, call {nameof(Build)} method first.");
}
