namespace BuilderDemo;

public interface IPageBuilderFluentApi
{
    void AddHeader();

    void AddTitle();

    void AddBody();

    void AddFooter();

    IPage GetPage();
}
