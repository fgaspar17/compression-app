using System.IO.Compression;

namespace CompressionApp.Domain.Services;

public class GzipDecompressionService
{
    public async Task DecompressFileAsync(string filePathOrigin, string filePathDestination, CancellationToken ct)
    {
        await using (var fileStreamOrigin = File.OpenRead(filePathOrigin))
        await using (var fileStreamDestination = File.OpenWrite(filePathDestination))
        await using (var gzipStream = new GZipStream(fileStreamOrigin, CompressionMode.Decompress))
        {
            await gzipStream.CopyToAsync(fileStreamDestination, ct);
        }
    }

    public async Task DecompressFileFromStreamAsync(Stream originStream, string filePathDestination, CancellationToken ct)
    {
        await using (var fileStreamDestination = File.OpenWrite(filePathDestination))
        await using (var gzipStream = new GZipStream(originStream, CompressionMode.Decompress))
        {
            await gzipStream.CopyToAsync(fileStreamDestination, ct);
        }
    }
}