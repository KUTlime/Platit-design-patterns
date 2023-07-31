namespace DependencyInjection.Demo;

public class SingletonInstance
{
    public int Id { get; set; } = new Random().Next(0, 1000);
}
