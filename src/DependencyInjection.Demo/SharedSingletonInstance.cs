namespace DependencyInjection.Demo;

public class SharedSingletonInstance
{
    public int Id { get; set; } = new Random().Next(0, 1000);
}
