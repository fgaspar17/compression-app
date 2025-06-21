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

    public async Task CompressFiles((string filename, Stream stream)[] originStreams, string filePathDestination)
    {
        if (originStreams.Length == 0)
            return;

        if (File.Exists(filePathDestination))
            File.Delete(filePathDestination);


        using FileStream destStream = File.OpenWrite(filePathDestination);
        using var zipArchive = new ZipArchive(destStream, ZipArchiveMode.Create, leaveOpen: false);
        foreach (var stream in originStreams)
        {
            var zipEntry = zipArchive.CreateEntry(stream.filename);
            using (var zipStream = zipEntry.Open())
            {
                byte[] buffer = new byte[1024];
                int reading = 1;
                while (reading != 0)
                {
                    reading = await stream.stream.ReadAsync(buffer, 0, buffer.Length);
                    await zipStream.WriteAsync(buffer, 0, reading);
                }

                await zipStream.FlushAsync();
            }
        }
    }
}