namespace DependencyInjection.Demo.Abstractions;

public interface IRepository
{
    object GetById(string id);
}

public interface IRepository<in T>
{
    object GetById(T id);
}
