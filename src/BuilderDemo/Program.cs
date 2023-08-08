var builder = new TitlePageBuilder();

var director = new Director(builder);
director.Build();
var page = director.GetPage();

var dir2 = new Director(builder);
dir2.Build();
Console.WriteLine(page.Header);
Console.WriteLine("----------------");
page = dir2.GetPage();
Console.WriteLine(page.Header);
Console.WriteLine("----------------");
page = director.GetPage();
Console.WriteLine(page.Header);
Console.WriteLine("----------------");

var dirWithFactory1 = new DirectorWithFactory(new DemoBuilderFactory(), "title");
page = dirWithFactory1.GetPage();
Console.WriteLine(page.Header);
Console.WriteLine("----------------");

var dirWithFactory2 = new DirectorWithFactory2(new DemoBuilderFactory());
dirWithFactory2.Build("title");
page = dirWithFactory2.GetPage();
Console.WriteLine(page.Header);
Console.WriteLine("----------------");

dirWithFactory2.Build("title");
page = dirWithFactory2.GetPage();
Console.WriteLine(page.Header);
Console.WriteLine("----------------");

var dirWithFactory3 = new DirectorWithFactory3(new DemoBuilderFactory());
page = dirWithFactory3.Build("title");

Console.WriteLine(page.Header);
Console.WriteLine("----------------");
