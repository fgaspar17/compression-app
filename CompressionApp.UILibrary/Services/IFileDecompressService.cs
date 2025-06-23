namespace CompressionApp.UILibrary.Services;

public interface IFileDecompressService
{
    Task<string> DecompressFiles(IEnumerable<(string filename, Stream stream)> filesPicked, CancellationToken ct);
}