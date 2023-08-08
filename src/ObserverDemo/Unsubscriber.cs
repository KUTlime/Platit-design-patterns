namespace ObserverDemo;

public class Unsubscriber : IDisposable
{
    private readonly IObserver<Tweet> _observer;
    private readonly HashSet<IObserver<Tweet>> _observers;

    public Unsubscriber(
        IObserver<Tweet> observer,
        HashSet<IObserver<Tweet>> observers)
    {
        _observer = observer;
        _observers = observers;
    }

    public void Dispose() => _observers.Remove(_observer);
}
