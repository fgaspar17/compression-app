using System.IO.Compression;

namespace CompressionApp.Domain;

public class ZipCompressionService
{
    public void CompressFiles(string[] filesPathOrigin, string filePathDestination)
    {
        if (filesPathOrigin.Length == 0)
            return;

        if (File.Exists(filePathDestination))
            File.Delete(filePathDestination);

        using var zipArchive = ZipFile.Open(filePathDestination, ZipArchiveMode.Create);
        foreach (var filePath in filesPathOrigin)
        {
            zipArchive.CreateEntryFromFile(filePath, Path.GetFileName(filePath), compressionLevel: CompressionLevel.Optimal);
        }
    }
}