using System.IO.Compression;

namespace CompressionApp.Domain;

public class GzipCompressionService
{
    public async Task CompressFileAsync(string filePathOrigin, string filePathDestination, CancellationToken ct)
    {
        await using (var fileStreamOrigin = File.OpenRead(filePathOrigin))
        await using (var fileStreamDestination = File.OpenWrite(filePathDestination))
        await using (var gzipStream = new GZipStream(fileStreamDestination, CompressionLevel.Optimal))
        {
            await fileStreamOrigin.CopyToAsync(gzipStream, ct);
        }
    }

    public async Task CompressFileAsync(Stream originStream, string filePathDestination, CancellationToken ct)
    {
        await using (var fileStreamDestination = File.OpenWrite(filePathDestination))
        await using (var gzipStream = new GZipStream(fileStreamDestination, CompressionLevel.Optimal))
        {
            await originStream.CopyToAsync(gzipStream, ct);
        }
    }
}