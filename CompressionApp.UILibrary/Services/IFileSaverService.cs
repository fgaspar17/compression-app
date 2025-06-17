namespace CompressionApp.UILibrary.Services;

public interface IFileSaverService
{
    Task<string?> SaveFileAsync(string filename, string directory, Stream inputStream, CancellationToken ct);
}