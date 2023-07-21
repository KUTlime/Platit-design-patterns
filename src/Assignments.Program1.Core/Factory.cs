namespace Assignments.Program1.Core;

public class Factory : IFactoryV1<IService>
{
    public IService CreateOrDefault(string discriminator) => discriminator switch
    {
        "public" => new PublicService(),
        "nonpublic" => new PrivateExpensiveService(),
        "private" => new PrivateExpensiveService(),
        "fast" => new PrivateExpensiveService(),
        _ => new PublicService(),
    };
}
