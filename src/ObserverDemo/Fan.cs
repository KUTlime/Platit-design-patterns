namespace ObserverDemo;

public class Fan : IObserver<Tweet>
{
    public Fan(string name) => Name = name;

    public string Name { get; }

    public void OnCompleted() => throw new NotImplementedException();

    public void OnError(Exception error) => throw new NotImplementedException();

    public void OnNext(Tweet tweet) => Console.WriteLine($"{Name}: I'm reading a tweet from {tweet.AuthorName} says: '{tweet.Message}'.");
}
