namespace DependencyInjection.Demo;

public class FakeRepository : IRepository, IRepository<Guid>
{
    public object GetById(string id)
    {
        Console.WriteLine("Faking search by string ID...");
        return new object();
    }

    public object GetById(Guid id)
    {
        Console.WriteLine("Faking search by GUID...");
        return new object();
    }
}
