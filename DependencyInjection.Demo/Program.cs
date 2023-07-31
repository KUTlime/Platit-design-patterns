var sharedSingletonInstance = new SharedSingletonInstance();
Console.WriteLine($"Shared singleton instance ID: {sharedSingletonInstance.Id}");

var serviceCollection = new ServiceCollection()
    .AddTransient<IRepository, Repository>()
    .AddTransient(typeof(IRepository<>), typeof(GenericRepository<>))
    .AddTransient<GenericRepository<int>, IntRepository>()
    .AddTransient<IRepository<Guid>, Repository>()
    .AddSingleton<SingletonInstance>()
    .AddSingleton<SharedSingletonInstance>(_ => sharedSingletonInstance)
    ;
var serviceProvider = serviceCollection
    .BuildServiceProvider();

var repository = serviceProvider.GetRequiredService<IRepository>();
repository.GetById("asdf");
var singleton = serviceProvider.GetRequiredService<SingletonInstance>();
Console.WriteLine(singleton.Id);
sharedSingletonInstance = serviceProvider.GetRequiredService<SharedSingletonInstance>();
Console.WriteLine($"Shared singleton instance ID: {sharedSingletonInstance.Id}");

serviceProvider = serviceCollection
    .RemoveAll<IRepository>()
    .AddTransient<IRepository, FakeRepository>()
    .BuildServiceProvider()
    ;

repository = serviceProvider.GetRequiredService<IRepository>();
repository.GetById("asdf");
singleton = serviceProvider.GetRequiredService<SingletonInstance>();
Console.WriteLine(singleton.Id);
sharedSingletonInstance = serviceProvider.GetRequiredService<SharedSingletonInstance>();
Console.WriteLine($"Shared singleton instance ID: {sharedSingletonInstance.Id}");

var sp = serviceCollection.BuildServiceProvider();
repository = sp.GetRequiredService<IRepository>();
repository.GetById("asdf");
singleton = serviceProvider.GetRequiredService<SingletonInstance>();
Console.WriteLine(singleton.Id);
singleton = sp.GetRequiredService<SingletonInstance>();
Console.WriteLine(singleton.Id);
sharedSingletonInstance = serviceProvider.GetRequiredService<SharedSingletonInstance>();
Console.WriteLine($"Shared singleton instance ID: {sharedSingletonInstance.Id}");
sharedSingletonInstance = sp.GetRequiredService<SharedSingletonInstance>();
Console.WriteLine($"Shared singleton instance ID: {sharedSingletonInstance.Id}");

var intRepository = serviceProvider.GetRequiredService<IRepository<int>>();
intRepository.GetById(1);
singleton = serviceProvider.GetRequiredService<SingletonInstance>();
Console.WriteLine(singleton.Id);
