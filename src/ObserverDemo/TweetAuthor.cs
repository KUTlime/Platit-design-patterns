namespace ObserverDemo;

public class TweetAuthor : IObservable<Tweet>
{
    private readonly HashSet<IObserver<Tweet>> _fans = new();

    public TweetAuthor(string name) => Name = name;

    public string Name { get; }

    public IDisposable Subscribe(IObserver<Tweet> observer)
    {
        _ = _fans.Add(observer);
        return new Unsubscriber(observer, _fans);
    }

    public void Tweet(string message)
    {
        foreach (var observer in _fans)
        {
            observer.OnNext(new Tweet(Name, message));
        }
    }
}
