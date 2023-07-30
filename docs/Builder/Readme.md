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
    void AddFooter()
    IPage GetPage();
}

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
    IPageBuilder AddFooter()
    IPage GetPage();
}

public class Director
{
    public readonly IPageBuilder _builder;

    public Director(IPageBuilder builder) => _builder = builder;

    public Build() => _builder.AddHeader().AddTitle().AddBody().AddFooter();

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
    public IPageBuilder Build(IPageBuilder builder) => builder.AddHeader().AddTitle().AddBody().AddFooter();
}

// Main
var builder = Director.Build(new MainHtmlPageBuilder());
var page = builder.GetPage();
```

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

Toto ovšem není moc šťastná varianta, protože někdo může zavolat `GetPage(...)` napřímo. Proto volíme spíše jiný postup, viz níže.

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
    IPage BuildPage() => AddHeader().AddTitle().AddBody().AddFooter().GetPage();
}

public class MainHtmlPageBuilder : PageBuilder
{
    // Implementace abstraktních metod
}

// Main
var page = new MainHtmlPageBuilder().BuildPage();
```

## Návaznost na další návrhové vzory

### Továrna + stavitel

Místo složitého vytváření objektů v továrně, můžeme vyrobit pouze správného stavitele a předat ho řediteli, tj. objektu, který továrnu volal.
