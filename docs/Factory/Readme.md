# Návrhový vzor Továrna

> Zjednodušuje vytváření složitých objektů.

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
    TOut Create(SomeEnum discriminator);
    TOut? CreateOrDefault(SomeEnum discriminator);
}
```

## Typické továrna

### Základní tvar

Trochu se nám tady bije `TOut?` s "Default" kontraktem. Musím kontrolovat na `null`.

```csharp
interface IFactory<TOut>
{
    TOut Create(string discriminator);
    TOut? CreateOrDefault(string discriminator);
}
```

### Základní tvar

Trochu se nám tady bije `TOut?` s "Default" kontraktem. Musím kontrolovat na `null`.

```csharp
interface IFactory<TIn, TOut>
{
    TOut Create(TIn discriminator);
    TOut? CreateOrDefault(TIn discriminator);
}
```

### Varianta s výchozí (ne `null`) hodnotou

Můžeme také zkombinovat s `Null` návrhovým vzorem (_typově silný ekvivalent `null`_).

```csharp
interface IFactory<TIn, TOut>
{
    TOut Create(TIn discriminator);
    TOut CreateOrDefault(TIn discriminator);
}
```

### Komplexní továrna


```csharp
interface IFactory<TIn, TOut>
{
    TOut CreateService(TIn discriminator);
    TOut CreateOrDefault(TIn discriminator);
    IEnumerable<TIn> Get
}
```
