public record CompressionTask(IEnumerable<string> FilePaths, ICompressStrategy Strategy)
{
    public string Compress() => Strategy.Compress(FilePaths);
}
