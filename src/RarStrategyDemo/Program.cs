string[] files = { "Test.txt", "Hello.txt" };
var task = new CompressionTask(files, new FastCompression());

string zipfile = await Task.Run(task.Compress);
