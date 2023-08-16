public interface ICompressStrategy
{
    string Compress(IEnumerable<string> pathsToFiles);
}
