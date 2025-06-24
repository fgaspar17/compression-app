using System.IO.Compression;

namespace CompressionApp.Domain.Tests;

public class ZipDecompressionServiceTests
{
    [Fact]
    public void DecompressFiles_ShouldExtractAllFiles()
    {
        // Arrange
        var zipService = new ZipCompressionService();
        var unzipService = new ZipDecompressionService();

        var tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);
        var file1 = Path.Combine(tempDir, "file1.txt");
        var file2 = Path.Combine(tempDir, "file2.txt");

        File.WriteAllText(file1, "File 1 content");
        File.WriteAllText(file2, "File 2 content");

        var zipPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");
        zipService.CompressFiles(new[] { file1, file2 }, zipPath);

        var extractPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        // Act
        unzipService.DecompressFiles(zipPath, extractPath);

        // Assert
        var extracted1 = Path.Combine(extractPath, "file1.txt");
        var extracted2 = Path.Combine(extractPath, "file2.txt");

        Assert.True(File.Exists(extracted1));
        Assert.True(File.Exists(extracted2));

        Assert.Equal("File 1 content", File.ReadAllText(extracted1));
        Assert.Equal("File 2 content", File.ReadAllText(extracted2));

        // Cleanup
        File.Delete(file1);
        File.Delete(file2);
        File.Delete(zipPath);
        Directory.Delete(tempDir, true);
        Directory.Delete(extractPath, true);
    }

    [Fact]
    public async Task DecompressFilesAsync_FromStream_ShouldExtractAllFiles()
    {
        // Arrange
        var zipService = new ZipCompressionService();
        var unzipService = new ZipDecompressionService();

        string fileName = "file.txt";
        string fileContent = "Stream-based zip content";

        var stream = new MemoryStream();
        using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, leaveOpen: true))
        {
            var entry = archive.CreateEntry(fileName);
            await using var writer = new StreamWriter(entry.Open());
            await writer.WriteAsync(fileContent);
        }

        stream.Position = 0;

        var extractPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());

        // Act
        await unzipService.DecompressFilesAsync(stream, extractPath, CancellationToken.None);

        // Assert
        var extractedFile = Path.Combine(extractPath, fileName);
        Assert.True(File.Exists(extractedFile));
        Assert.Equal(fileContent, File.ReadAllText(extractedFile));

        // Cleanup
        Directory.Delete(extractPath, true);
    }
}
