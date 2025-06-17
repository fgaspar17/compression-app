using System.IO.Compression;

namespace CompressionApp.Application;

public class GzipCompressionService
{
    public async Task CompressFileAsync(Stream inputStream, string filePathDestination)
    {
        using FileStream destinationStream = new (filePathDestination, FileMode.CreateNew);
        using GZipStream compressionStream = new(destinationStream, CompressionLevel.Optimal);
        await inputStream.CopyToAsync(compressionStream);
    }
}