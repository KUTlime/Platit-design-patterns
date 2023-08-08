namespace BuilderDemo;

public class TitlePageBuilder : IPageBuilder
{
    private readonly List<string> _headers = new();

    public void AddHeader() => _headers.Add("This is title page.");

    public void AddTitle()
    {
    }

    public void AddBody()
    {
    }

    public void AddFooter()
    {
    }

    public IPage GetPage() => new TitlePage { Header = string.Join("\n", _headers) };
}
