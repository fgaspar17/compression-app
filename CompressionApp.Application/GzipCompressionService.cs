using System.IO.Compression;

namespace CompressionApp.App;

public class GzipCompressionService
{
    public async Task<Stream> GetCompressionStreamAsync(string filePath)
    {
        var memoryStream = new MemoryStream();

        await using (var fileStream = File.OpenRead(filePath))
        await using (var gzipStream = new GZipStream(memoryStream, CompressionLevel.Optimal, leaveOpen: true))
        {
            await fileStream.CopyToAsync(gzipStream);
        }

        memoryStream.Position = 0; // Important to reset position
        return memoryStream;
    }
}