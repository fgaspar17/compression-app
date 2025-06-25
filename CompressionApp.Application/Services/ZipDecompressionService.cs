using System.Diagnostics;
using System.IO.Compression;

namespace CompressionApp.Domain.Services;

public class ZipDecompressionService
{
    public void DecompressFiles(string filePathOrigin, string folderPathDestination)
    {
        using var archive = ZipFile.OpenRead(filePathOrigin);
        archive.ExtractToDirectory(folderPathDestination);
    }

    public async Task DecompressFilesAsync(Stream originStream, string folderPathDestination, CancellationToken ct)
    {
        // Copy to a MemoryStream, because the Zip library doesn't support async operations
        var ms = new MemoryStream();
        await originStream.CopyToAsync(ms, ct);
        ZipFile.ExtractToDirectory(ms, folderPathDestination);
    }
}