namespace BuilderDemo;

public class DemoBuilderFactory : IFactory<string, IPageBuilder>
{
    public IPageBuilder Create(string id) => id.ToLowerInvariant() switch
    {
        "title" => new TitlePageBuilder(),
        _ => throw new NotSupportedException("Not supported discriminator."),
    };
}
