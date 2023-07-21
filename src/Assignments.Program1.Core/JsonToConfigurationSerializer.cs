namespace Assignments.Program1.Core;

public static class JsonToConfigurationSerializer
{
    public static IEnumerable<ConfigKey> Serialize(string json)
    {
        _ = json;
        return Enumerable.Empty<ConfigKey>();
    }
}
