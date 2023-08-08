# Návrhový vzor Stavitel

> Stejný proces vytváření pro různé implementace.

Základem stavitele je šablonovité vytváření různých objektů sestávajících se ze stejných kroků.

Analogie s domem - každý dům potřebuje základní desku, připojit vodu, elektřinu, obvodové zdivo, malbu atd.

Kroky jsou stále stejné, implementace různá (_někdy tragická_).

## Složení (dle teorie GoF)

Pracujeme s dvěma objekty - **stavitelem** a **ředitelem**. Stavitel říká jak, ředitel říká kdy.

Ředitel přijímá instanci stavitele a ví, jaké metody má v jakém pořadí volat.

## Příklady použití

* Vytváření tiskových sestav
* Konfigurace aplikací (_ASP.NET Core_)
* Konfigurace Dependency Injection
* Sestavení košíku objednávky

## Praktická implementace v `C#`

* Dle situace používáme rozhraní, nebo abstraktní třídu, nebo obojí.
* Rozhraní používáme ve chvíli, kdy nepotřebujeme pracovat s žádným stavem (_v praxi málo kdy_).
* Rozhraní můžeme využít pro dependency injection.

## Základní implementace

```csharp
interface IPage
{}

interface IPageBuilder
{
    void AddHeader();
    void AddTitle();
    void AddBody();
    void AddFooter();
    IPage GetPage();
}

public class Director
{
    private readonly IPageBuilder _builder;

    public Director(IPageBuilder builder) => _builder = builder;

    public void Build()
    {
        _builder.AddHeader();
        _builder.AddTitle();
        _builder.AddBody();
        _builder.AddFooter();
    }

    public IPage GetPage() => _builder.GetPage();
}
```

## Základní implementace s využitím fluent API

```csharp
interface IPage
{}

interface IPageBuilder
{
    IPageBuilder AddHeader();
    IPageBuilder AddTitle();
    IPageBuilder AddBody();
    IPageBuilder AddFooter();
    IPage GetPage();
}

public class Director
{
    public readonly IPageBuilder _builder;

    public Director(IPageBuilder builder) => _builder = builder;

    public void Build() => _builder.AddHeader().AddTitle().AddBody().AddFooter();

    IPage GetHTMLPage() => _builder.GetPage();
}
```

## Implementace se statickou build metodou

```csharp
interface IPage
{}

interface IPageBuilder
{
    IPageBuilder AddHeader();
    IPageBuilder AddTitle();
    IPageBuilder AddBody();
    IPageBuilder AddFooter()
    IPage GetPage();
}

public static class Director
{
    public static IPageBuilder Build(IPageBuilder builder) 
        => builder.AddHeader().AddTitle().AddBody().AddFooter();
}

// Main
var builder = Director.Build(new MainHtmlPageBuilder());
var page = builder.GetPage();
```

Statický přístup je vhodný použít tehdy, kdy mám v rámci aplikace jedno konkrétní volání, nebo bude vždy stejné, nepotřebuji mockovat, nebo z nějakého jiného důvodu se vždy obejdu se statickou implementací.

## Zjednodušená varianta bez ředitele alá >=`C# 8`

```csharp
interface IPage
{}

interface IPageBuilder
{
    IPageBuilder AddHeader();
    IPageBuilder AddTitle();
    IPageBuilder AddBody();
    IPageBuilder AddFooter();
    IPage GetPage();
    IPage BuildPage() => AddHeader().AddTitle().AddBody().AddFooter().GetPage();
}


// Main
var page = new MainHtmlPageBuilder().Build();
```

Toto ovšem není moc šťastná varianta, protože někdo může zavolat `GetPage(...)` napřímo (_popř. zavolat nějakou jinou metodu z rozhraní i když to tak být nemá_). Proto volíme spíše jiný postup, viz níže.

## Varianta bez ředitele s pomocí abstraktní třídy

```csharp
interface IPage
{}

public abstract class PageBuilder
{
    protected abstract IPageBuilder AddHeader();
    protected abstract IPageBuilder AddTitle();
    protected abstract IPageBuilder AddBody();
    protected abstract IPageBuilder AddFooter();
    protected abstract IPage GetPage();
    public IPage BuildPage() => AddHeader().AddTitle().AddBody().AddFooter().GetPage();
}

public class MainHtmlPageBuilder : PageBuilder
{
    // Implementace abstraktních metod
}

// Main
var page = new MainHtmlPageBuilder().BuildPage();
```

Pokud by metody, které volám měly nějaké parametry, tak je musím předat nejpozději při volání `BuilPage(...)`.

Lze řešit formou specifických argumentů.

```csharp
interface IPage
{}

public record PageBuilderArgs(string Header, string Title, string Body, string Footer);

public abstract class PageBuilder
{
    protected abstract IPageBuilder AddHeader();
    protected abstract IPageBuilder AddTitle();
    protected abstract IPageBuilder AddBody();
    protected abstract IPageBuilder AddFooter();
    protected abstract IPage GetPage();
    public virtual IPage BuildPage(PageBuilderArgs args) 
        => AddHeader(args.Header).AddTitle(args.Title).AddBody(args.Body).AddFooter(args.Footer).GetPage();
}

public class MainHtmlPageBuilder : PageBuilder
{
    // Implementace abstraktních metod
}

// Main
var page = new MainHtmlPageBuilder()
    .BuildPage(new PageBuilderArgs("1/1", "Welcome", string.Empty, string.Empty));
```

## Návaznost na další návrhové vzory

### Továrna + stavitel

Místo složitého vytváření objektů v továrně, můžeme vyrobit pouze správného stavitele a předat ho řediteli, tj. objektu, který továrnu volal.

```csharp
interface IPage
{}

interface IPageBuilder
{
    IPageBuilder AddHeader();
    IPageBuilder AddTitle();
    IPageBuilder AddBody();
    IPageBuilder AddFooter();
    IPage GetPage();
    IPage BuildPage() => AddHeader().AddTitle().AddBody().AddFooter().GetPage();
}

interface IPageBuilderFactory
{
    IPageBuilder Create(string discriminator);
}

public class HtmlPageBuilderFactory : IPageBuilderFactory
{
    // Implementace interface
}

// Main
string builderDiscriminator = "html";
var page = new HtmlPageBuilderFactory().Create(builderDiscriminator).BuildPage();

```

popř. pro C# 11 a vyšší

```csharp
interface IPage
{}

interface IPageBuilder
{
    IPageBuilder AddHeader();
    IPageBuilder AddTitle();
    IPageBuilder AddBody();
    IPageBuilder AddFooter();
    IPage GetPage();
    IPage BuildPage() => AddHeader().AddTitle().AddBody().AddFooter().GetPage();
}

interface IPageBuilderFactory
{
    static abstract IPageBuilder Create(string discriminator);
}

public class PageBuilderFactory : IPageBuilderFactory
{
    // Implementace interface
}

// Main
var page = PageBuilderFactory.Create("html").BuildPage();

```
