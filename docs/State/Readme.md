# Stav

> Změna chování třídy na základě **vnitřního** stavu třídy.

Cílem Stavu je vytvořit závislost chování třídy na jejím vnitřním stavu, zjednodušit vnitřní implementaci (_odstranit `if`y_) a celkově zpřehlednit kód implementací jednotlivých stavových chování do samostatných tříd.

Tento vzor je podobný se strategií, kde však chování určuje klient (_je vnuceno z venku_).

## Složení (dle GoF)

1. Vícestavová třída (_konzument stav_)
2. Jednostavová třída (_stav samotný_)

## Princip

Stav třídy modelujeme konkrétní implementací rozhraní (_častěji_) nebo abstraktní třídy. Na základě vnitřního podnětu dochází ke změně implementace a tím pádem změně chování objektu jako celku.

Konkrétní chování se tak odděluje do samostatných implementací, které tímto zapouzdřujeme.

Přidání stavu nesmí ovlivnit konzumenta.

## Použití

* Jakékoliv stavem podmíněné chování.
* Packman (_a obecně game development, kde se různé objekty, typicky "umělá" inteligence může chovat jinak podle situace - počet přátel v okolí, stav munice, stav zdraví atd._)
* Pohyb figurek po šachovnici (_typicky král_)
* Možnost implementace stavového stroje (_stav 1 -> stav 2 -> stav 3), nebo (_stav 1 -> stav 2 -> stav 1_)

### Příklad hry ze světa Star Treku

Představme si příklad, že modelujeme tahovou strategii ze světa Star Treku, kde se střetávají dvě kosmické lodě. Zde využijeme stav hned několik stavů.

1. Volba manévrování na základě stavu štítů (natočení lodi směrem k minimalizaci poničení lodi).
2. Volba útoku na základě počtu cílů, jejich poškození, a zásob (_např. photonových torpéd_).
3. Modelování damage lodi, na kterou je útočeno. Loď má např. rychlost = 0, pokud stav motorů = mimo provoz.

## Příklad implementace

```csharp
public interface IState
{
    bool CanMove();
}

public class Pawn
{
    private IState State = new InitialState();
}
```
