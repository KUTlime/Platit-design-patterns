# Pozorovatel

> Komunikace ve stylu jeden (_vydavatel_) k mnoha (_posluchačům_).

Tento návrhový vzor řeší komunikaci ve vztahu 1:N (_jeden k mnoha_).

## Složení (dle GoF)

Pracujeme opět s dvěma objekty:

1. Vydavatel (pozorovaný), anglicky subject.
2. Předplatitel (pozorovatel), anglicky observer.

## Princip

Pozorovaný uchovává seznam pozorovatelů a případně potřeby, vyvolá na pozorovateli rozhraní, kterým informuje o změně.

Pozorovaný poskytuje rozhraní pro zápis a vyškrtnutí ze seznamu pozorovaných.

## Použití

* Potřeba změnit/informovat mnoho objektů naráz.
* Potřeba změnit/informovat blíže neznámý počet objektů naráz.
* Potřeba změnit/informovat blíže neznámé objekty.

Prakticky to znamená např. naslouchat na změny nastavení, změny stavu zásob na skladě, připojení I/O zařízení, RSS čtečka atd.

Obecně lze říci, že se jedná o variaci návrhového vzoru [Publish-subscribe](https://en.wikipedia.org/wiki/Publish%E2%80%93subscribe_pattern).

Aplikace s grafickým uživatelským rozhraním se bez tohoto mechanismu neobejdou. Zde využíváme událostmi řízený software.

## Implementace s silnou vazbou

"Standardní" implementace vede na úzkou vazbu mezi pozorovaným a pozorovatelem. Jeden musí znát druhého, čili úzká vazba.

### Vlastní vzorová implementace

```csharp
    public class Payload
    {
        public string Message { get; set; }
    }

    public class Subject : IObservable<Payload>
    {
        public ICollection<IObserver<Payload>> Observers { get; set; }

        public Subject() => Observers = new List<IObserver<Payload>>();

        public IDisposable Subscribe(IObserver<Payload> observer)
        {         
            if (!Observers.Contains(observer))
            {
                Observers.Add(observer);
            }

            return new Unsubscriber(observer, Observers);
        }

        public void SendMessage(string message)
        {
            foreach (var observer in Observers)
            {
                observer.OnNext(new Payload { Message = message });
            }
        }
    }

    public class Unsubscriber : IDisposable
    {
        private IObserver<Payload> _observer;
        private IList<IObserver<Payload>> _observers;

        public Unsubscriber(
            IObserver<Payload> observer,
            IList<IObserver<Payload>> observers)
        {
            _observer = observer;
            _observers = observers;
        }

        public void Dispose()
        {
            if (observer != null && observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }
    }

    public class Observer : IObserver<Payload>
    {
        public string Message { get; set; }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(Payload value) => Message = value.Message;

        public IDisposable Register(Subject subject) => subject.Subscribe(this);
    }
```

To v praxi moc nechceme, ale můžeme řešit jinak.

## Implementace se slabou vazbou

Závislosti můžeme snadno rozbít s pomocí generiky.

### Generická implementace

```csharp
    public class Payload
    {
        public string Message { get; set; }
    }

    public class Subject<T> : IObservable<T>
    {
        public ICollection<IObserver<T>> Observers { get; set; }

        public Subject() => Observers = new List<IObserver<T>>();

        public IDisposable Subscribe(IObserver<T> observer)
        {         
            if (!Observers.Contains(observer))
            {
                Observers.Add(observer);
            }

            return new Unsubscriber(observer, Observers);
        }

        public void SendMessage(string message)
        {
            foreach (var observer in Observers)
            {
                observer.OnNext(new Payload { Message = message });
            }
        }
    }

    public class Unsubscriber<T> : IDisposable
    {
        private IObserver<T> _observer;
        private IList<IObserver<T>> _observers;

        public Unsubscriber(
            IObserver<T> observer,
            IList<IObserver<T>> observers)
        {
            _observer = observer;
            _observers = observers;
        }

        public void Dispose()
        {
            if (observer != null && observers.Contains(observer))
            {
                observers.Remove(observer);
            }
        }
    }

    public class Observer<T> : IObserver<T>
    {
        public string Message { get; set; }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(T value) => Message = value.Message;

        public IDisposable Register(Subject subject) => subject.Subscribe(this);
    }
```

## Implementace na úrovni .NET

### Pozorovatel skrze rozhraní `IObservable<T>` a `IObserver<T>`

[Microsoft Learn článek](https://learn.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern)
[IObserver](https://learn.microsoft.com/en-us/dotnet/api/system.iobserver-1)
[IObservable](https://learn.microsoft.com/en-us/dotnet/api/system.iobservable-1)

## Implementace na úrovni jazyka

V C# můžeme použít události.

### Definice události

```csharp
using System;

class Program
{
    static void Main(string[] args)
    {
        var publisher = new Publisher();
        var subscriber = new Subscriber();

        publisher.OnEvent += subscriber.HandleEvent;

        publisher.RaiseEvent("Hello, World!");
    }
}

class Publisher
{
    public event EventHandler<string> OnEvent;

    public void RaiseEvent(string message)
    {
        var event = OnEvent;
        event?.Invoke(this, message);
    }
}

class Subscriber
{
    public void HandleEvent(object sender, string message)
    {
        Console.WriteLine($"Received message: {message}");
    }
}

```

## Problém tmavé posluchárny

Představme si situaci, že přednášíme v tmavé místnosti hvězdárny. Na začátku, než jsme zhasli, viděli jsme všechny posluchače v místnosti.

Předsálí je také potemněné, takže nevidíme, když někdo odchází z přednáškové místnosti do předsálí.

Naše téma zrovna moc netáhne, takže se lidé pomalu začínají zvedat a odcházet. Exodus bude tak masivní, že si nevšimneme, že už nemáme komu přednášet, takže vesele přednášíme dál a není zde nikdo, kdo by nás zastavil.

V OOP jazycích s automatickou správou paměti může nastat podobná situace, kdy pozorovaný hromadí ukazatele na pozorovatele. V C# je seznam pozorovatelů u události statický seznam, tudíž prodlužuje do nekonečna živnost referencí objektů, které jsou předány. Samotné objekty už by měly být zničeny, ale pozorovaný v brání tomuto procesu a dochází k měkkému úniku paměti.

V angličtině se tomuto problému říká [Lapsed listener problem](https://en.wikipedia.org/wiki/Lapsed_listener_problem).

Situaci se bráníme tak, že musíme implementovat i `-=` chování, ne jenom `+=`.



