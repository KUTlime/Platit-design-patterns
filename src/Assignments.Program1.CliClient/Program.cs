Console.WriteLine(args[0]); // Path to json

/*Načítat cestu nějakého JSON, kde něco najdete, něco s tím provedete.
 * Nějaká další logika, validace a co já vím co...
 * Key: Serilog:WriteTo
 * Value: Console
 * Key: Serilog:Color
 * Value: Native
 */

string json = File.ReadAllText(args[0]);
var keys = JsonToConfigurationSerializer.Serialize(json);
var client = new ConfigProviderWraper(
    new ConfigProviderClient(
        new ApiConfiguration(new Uri(args[1]), args[2], args[3])));
var uploader = new ConfigUploader(client);
var result = uploader.Upload(keys);

Console.WriteLine(result);
