namespace DependencyInjection.Demo;

public class Repository : IRepository, IRepository<Guid>
{
    public object GetById(string id)
    {
        Console.WriteLine("Searching by string ID in database...");
        return new object();
    }

    public object GetById(Guid id)
    {
        Console.WriteLine("Searching by GUID in database...");
        return new object();
    }
}
