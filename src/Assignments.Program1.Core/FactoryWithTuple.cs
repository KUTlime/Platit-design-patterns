namespace Assignments.Program1.Core;

public class FactoryWithTuple : IFactoryV1<(IService, bool)>
{
    public (IService, bool) CreateOrDefault(string discriminator) => discriminator switch
    {
        "public" => (new PublicService(), true),
        "nonpublic" => (new PrivateExpensiveService(), true),
        "private" => (new PrivateExpensiveService(), true),
        "fast" => (new PrivateExpensiveService(), true),
        _ => (new PublicService(), false),
    };
}
