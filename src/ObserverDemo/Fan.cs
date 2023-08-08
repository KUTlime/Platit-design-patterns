namespace ObserverDemo;

public class Fan : IObserver<Tweet>
{
    public void OnCompleted() => throw new NotImplementedException();

    public void OnError(Exception error) => throw new NotImplementedException();

    public void OnNext(Tweet tweet) => Console.WriteLine($"I'm reading a tweet from {tweet.AuthorName} says: '{tweet.Message}'.");
}
