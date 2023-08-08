namespace BuilderDemo;

public interface IPageBuilderFluentApi
{
    IPageBuilderFluentApi AddHeader();

    IPageBuilderFluentApi AddTitle();

    IPageBuilderFluentApi AddBody();

    IPageBuilderFluentApi AddFooter();

    IPage GetPage();
}
