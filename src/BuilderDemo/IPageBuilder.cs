namespace BuilderDemo;

public interface IPageBuilder
{
    void AddHeader();

    void AddTitle();

    void AddBody();

    void AddFooter();

    IPage GetPage();
}
