using System.IO.Compression;

namespace CompressionApp.Domain;

public class GzipCompressionService
{
    public async Task GetCompressionStreamAsync(string filePathOrigin, string filePathDestination, CancellationToken ct)
    {
        await using (var fileStreamOrigin = File.OpenRead(filePathOrigin))
        await using (var fileStreamDestination = File.OpenWrite(filePathDestination))
        await using (var gzipStream = new GZipStream(fileStreamDestination, CompressionLevel.Optimal, leaveOpen: true))
        {
            await fileStreamOrigin.CopyToAsync(gzipStream, ct);
        }
    }
}