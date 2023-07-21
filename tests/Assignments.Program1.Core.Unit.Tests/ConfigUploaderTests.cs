namespace Assignments.Program1.Core.Unit.Tests;

public static class ConfigUploaderTests
{
    public class UploadTests
    {
        [Fact]
        public async Task BasicSerializationTest()
        {
            string json = "{}";
            var keys = JsonToConfigurationSerializer.Serialize(json);
            var testClient = new TestConfigProviderClient(r => r.Encrypted == false);
            var uploader = new ConfigUploader(testClient);
            bool result = await uploader.Upload(keys);
            _ = result.Should().BeTrue();
        }

        [Fact]
        public async Task AdvancedSerializationTest()
        {
            _ = /*lang=json,strict*/ """
                {
                  "Serilog": {
                    "WriteTo": "Console",
                    "Color": "Native"
                  }
                }
                """;
            var testClient = new TestConfigProviderClient(r => r.Encrypted == false);
            var uploader = new ConfigUploader(testClient);
            bool result = await uploader.Upload(new[] { new ConfigKey("test", "test") });
            _ = result.Should().BeTrue();
            _ = testClient.AssertionResult.Should().BeTrue();
        }
    }
}
