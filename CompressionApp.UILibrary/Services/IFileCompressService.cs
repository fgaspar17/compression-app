namespace CompressionApp.UILibrary.Services;

public interface IFileCompressService
{
    Task<string> CompressFiles(IEnumerable<(string filename, Stream stream)> filesPicked, CancellationToken ct);
}
