namespace Assignments.Program1.Core.Unit.Tests;

public static class JsonToConfigurationSerializerTests
{
    public class SerializeTests
    {
        [Fact]
        public void SimpleEmptyValueTest()
        {
            var result = JsonToConfigurationSerializer.Serialize("{}");
            _ = result.Should().BeEmpty();
        }
    }
}
