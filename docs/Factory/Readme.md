# Návrhový vzor Továrna

> Zjednodušuje vytváření složitých objektů.

Továrna na základě vstupního diskriminátoru poskytne klientovi konkrétní objekt, nejčastěji konkrétní implementaci abstraktní třídy, nebo rozhraní.

## Složení

Pracujeme pouze s jedním objektem - továrnou, která metodu `Create(...)`, `Get(...)`, `CreateService(...)`, nebo podobnou metodu.

## Špatný přístup

```csharp
var car = new Car(new Motor(new Engine(new Oil(new WeatherCondition()))));
```

Vidíme, že pro vytvoření vysokoúrovňového objektu - auto, musíme znát implementační detaily celého objektu a dát tam všechno až po druh oleje podle počasí.

Toto je v praxi nepoužitelný a neudržitelný přístup.

## Červená linie

Mezi klienty továrny a implementačními detaily objektů, které továrna poskytuje, musíme nakreslit červenou linii, která odděluje implementační detaily objektů od toho, jak jsou objekty vytvářeny a zamezit pronikání jakýkoliv implementačních detailů ven.

V překladu to znamená, že nesmíme prozradit nic o tom, jaké objekty máme k dispozici a jak vnitřně fungují.

Prakticky to znamená, že můžeme vyměnit knihovnu, který továrnu poskytuje a zároveň objekty, které se továrnou vyrábí bez toho, aniž bych musel měnit jakkoliv klienta, který továrnu konzumuje. Získáme tím **Independent deployability** - nezávislé nasazení.

## Špatná implementace továrny

Nemůžeme vytvářet objekty s pomocí enumu, protože porušujeme "červenou linii implementačních detailů". Enum prozrazuje, kolik máme možností a pokud chceme rozšířit možnosti továrny, musíme rozšířit enum, což znamená i klienta, který s enumem pracuje.

```csharp
interface IFactory<TOut>
{
    TOut? Create(SomeEnum discriminator);
    TOut CreateOrDefault(SomeEnum discriminator);
}
```

## Typické továrna

### Základní tvar

Trochu se nám tady bije `TOut?` s "Default" kontraktem. Klient musí kontrolovat `null`, ale jde to vyřešit i jinak.

Typ `string` snese všechno, takže neprozrazuje žádné implementační detaily a **neporušuje implementační detaily**.

```csharp
interface IFactory<TOut>
{
    TOut? Create(string discriminator);
    TOut CreateOrDefault(string discriminator);
}
```

### Základní tvar bez fallback hodnoty

```csharp
interface IFactory<TOut>
{
    TOut Create(string discriminator);
}

class SomeFactory : IFactory<Foo>
{
    // In implementation, we should check possible null return value.
    // On the other hand... Clients will be forced to try/catch. 🤷🏿‍♀️
    Foo Create(string discriminator) => ... ?? throw new ResourceNotFoundException();
}
```

## Diskuze: `Null` nebo výjimky?

Nabízí se otázka: "Co je lepší? Vracet `null`, nebo vyhazovat výjimku? Nebo jinak?"

1. Pokud rozumíte jak se pracuje s nullable odkazovými typy, vracet `null` asi dává smysl. (nejlepší podpora v současném C#).
2. Pokud umíme pracovat s discriminated unions, asi je lepší vracet něco jako OneOf<SomeService, Nothing>.
3. Pokud víme, že je vhodnější aplikaci sestřelil, výjimka je na místě.
4. Další možnost je `Null pattern`.
5. Obecně je potřebné zvážit kontext, není jediné správné řešení.

## Vhodná slova pro továrnu

Továrna tvoří (_vytváří_) nějaké objekty. Slovo "object" je poněkud zavádějí, takže můžeme používat v názvech továren jiné synonymum.

* Service
* Resource

### Generický základní tvar

Nahradíme string discriminator za generický typ.

```csharp
interface IFactory<TIn, TOut>
{
    TOut? Create(TIn discriminator);
    TOut CreateOrDefault(TIn discriminator);
}
```

### Generický základní tvar s "službou"

Přidáme, že chceme pracovat s jakými "službami", ale přidání slovíčka "Service" do názvu je diskutabilní (_je to sice explicitní, ale dlouhé, při volání většinou duplicitní informace `ServiceFactory.CreateService(...)`_)

```csharp
interface IServiceFactory<TIn, TOut>
{
    TOut CreateService(TIn serviceRequest);
    TOut? CreateOrDefaultService(TIn serviceRequest);
}
```

Popř. negenerická varianta.

```csharp
interface IServiceFactory<TService>
{
    TService CreateService(string service);
    TService? CreateOrDefaultService(string service);
}
```

### Varianta s výchozí (ne `null`) hodnotou

Můžeme také zkombinovat s [`Null` návrhovým vzorem](https://en.wikipedia.org/wiki/Null_object_pattern) (_typově silný ekvivalent `null`_).

```csharp
interface IFactory<TIn, TOut>
{
    TOut Create(TIn discriminator);
    TOut CreateOrDefault(TIn discriminator);
}
```

### Komplexní továrna

Pro flexibilní design, který má vydržet časový test, většinou továrnu vybavíme metodami pro vrácení kompletního seznamu validních identifikátorů objektů, které továrna vyrábí.

```csharp
interface IFactory<TIn, TOut>
{
    TOut? Create(TIn discriminator);
    TOut CreateOrDefault(TIn discriminator);
    IEnumerable<TIn> GetService();
}
```

Opět se můžeme bavit o tom, jestli `GetService()`, nebo `Get()`, nebo ...

Stejně tak se můžeme bavit, jestli `GetService()` nebo `GetServices()` atd.

V praxi statickou továrnu moc často nepoužíváme, kvůli své "statické" podstatě, ale spíše používáme silně typově podepsanou továrnou a dependency injection.
