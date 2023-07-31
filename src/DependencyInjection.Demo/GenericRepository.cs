namespace DependencyInjection.Demo;

public class GenericRepository<T> : IRepository<T>
{
    public object GetById(T id)
    {
        Console.WriteLine($"Searching by {typeof(T).FullName} ID...");
        return new object();
    }
}
