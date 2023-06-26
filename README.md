# Návrhové vzory pro Platit

> Poznámky pro školení návrhové vzory na míru.

## Axiomy školení

* .NET 7 a vyšší
* C# 11
* Nullable Reference typy

## Lekce 1 (2023-06-25)

* Organizační záležitosti
* Mapování vstupních znalostí
* Co je potřeba opravdu znát při uvažování o implementaci návrhových vzorů
* Úvod do návrhových vzorů
* Factory
* Builder
* Úvod do Dependency Injection

## Organizační záležitosti

* Seznámení
* Pauzy, oběd
* Přístup do repositáře

## Mapování vstupních znalostí

### Program č. 1

Napište program, který pro čísla od 1 do 100 vytiskne:

* Slovo "Pla" pokud číslo je dělitelné 3,
* slovo "tit" pokud je číslo dělitelné 5,
* slovo "Platit" pokud je číslo dělitelné 3 i 5 zároveň,
* číslo pokud neplatí žádná výše zmíněná podmínka.

Výstup

```log
1
2
Pla
4
tit
Pla
7
8
Pla
tit
11
Pla
13
14
Platit
16
...
97
98
Pla
Tit
```

### Program č. 2

Představte si následující situaci:

Píšete konzolovou aplikaci, která bude používat třídu s názvem `ConfigProviderClient`. Tato třída pochází z knihovny 3. strany, ke zdrojovému kódu nemáte přístup a nemůžete ho přímo změnit. V třídě `ConfigProviderClient` budete volat metodu `AddConfigurationAsync(...)`

Struktura třídy `ConfigProviderClient` je naznačena níže.

```csharp
public record Request(Uri ConfigProviderUri, string ServiceName, string Token);

public class ConfigProviderClient
{
    public async Task<bool> AddConfigurationAsync(Request request)
    {
        // implementační detail
        // volání externího API skrze HTTP klienta
    }
}
```

Vaše konzolová aplikace musí sesbírat parametry z příkazové řádky, přečíst obsah nějakého JSON, něco s ním provést a vytvořit správný dotaz směrem k externímu API, které se volá uvnitř `ConfigProviderClient`, v metodě `AddConfigurationAsync(...)`.

Otázka zní: **Jak zařídíte, že můžete napsat test, který ověří, že Vaše aplikace pracuje správně a sestavuje správně požadavek?**

Nezajímá mne konkrétní implementace nějaké aplikace, ale jak si poradíte se situací, že musíte volat nějaké externí API v rámci testu.

## Co je opravdu potřeba znát při návrhu návrhových vzorů

* Rozhraní
* Abstraktní třída
* Rozdíl mezi rozhraním a abstraktní třídou
* Rozdíl mezi statickou a nestatickou třídou (_a metodou_)
* Jak funguje a jak se tvoří metoda rozšíření
* Jak funguje dědičnost (_a jaký je rozdíl mezi public, protected, private atd._)

## Úvod do návrhových vzorů

* Gang of Four
* Kniha [Design Patterns](https://www.amazon.com/Design-Patterns-Elements-Reusable-Object-Oriented/dp/0201633612)
* 3 kategorie návrhových vzorů - návrhové vzory vytváření, struktury a chování.
* Dnes známe mnoho dalších návrhových vzorů.
* Dnes máme dokonce i nové kategorie návrhových vzorů.
* Vznikají redesigny návrhových vzorů, např. populární Mediator v ASP.NET Core.
* Důležité je říci, že návrhové vzory z dílny GoF bude .NET vývojář spíše konzumovat, než vytvářet
  * `Task.Factory(...)` továrna na úkoly.
  * `Task.Run(...)` fasáda pro továrnu na úkoly.
  * atd.
