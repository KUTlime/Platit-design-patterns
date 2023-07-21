namespace Assignments.Program1.Core;

internal class FactoryWithDiscriminatedUnion : IFactoryV1<ServiceResult>
{
    public ServiceResult CreateOrDefault(string discriminator) => discriminator switch
    {
        "public" => new PublicService(),
        "nonpublic" => new PrivateExpensiveService(),
        "private" => new PrivateExpensiveService(),
        "fast" => new PrivateExpensiveService(),
        _ => default(OneOf.Types.None),
    };
}
