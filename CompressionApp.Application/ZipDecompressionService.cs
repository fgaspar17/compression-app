using System.IO.Compression;

namespace CompressionApp.Domain;

public class ZipDecompressionService
{
    public void DecompressFiles(string filePathOrigin, string folderPathDestination)
    {
        var archive = ZipFile.OpenRead(filePathOrigin);
        archive.ExtractToDirectory(folderPathDestination);
    }
}