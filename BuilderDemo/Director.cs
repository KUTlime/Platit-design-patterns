namespace BuilderDemo;

public class Director
{
    public readonly IPageBuilder _builder;

    public Director(IPageBuilder builder) => _builder = builder;

    public Build()
    {
        _builder.AddHeader();
        _builder.AddTitle();
        _builder.AddBody();
        _builder.AddFooter();
    }

    IPage GetPage() => _builder.GetPage();
}
