namespace Assignments.Program1.Core.Unit.Tests;

public class FactoryClientTests
{
    public class CreateTests
    {
        [Fact]
        public void Test()
        {
            var factoryClient = new FactoryClient();
            _ = factoryClient.Create("test");
        }
    }
}
