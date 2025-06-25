using System.IO.Compression;

namespace CompressionApp.Domain.Tests.Services;

public class ZipCompressionServiceTests
{
    [Fact]
    public void CompressFiles_WithMultipleFiles_ShouldCreateZipWithEntries()
    {
        // Arrange
        var zipService = new ZipCompressionService();

        string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempDir);

        string file1Path = Path.Combine(tempDir, "file1.txt");
        string file2Path = Path.Combine(tempDir, "file2.txt");

        File.WriteAllText(file1Path, "Content for file 1");
        File.WriteAllText(file2Path, "Content for file 2");

        string zipPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");

        // Act
        zipService.CompressFiles(new[] { file1Path, file2Path }, zipPath);

        // Assert
        Assert.True(File.Exists(zipPath));

        using (var zip = ZipFile.OpenRead(zipPath))
        {
            Assert.Equal(2, zip.Entries.Count);
            Assert.Contains(zip.Entries, e => e.Name == "file1.txt");
            Assert.Contains(zip.Entries, e => e.Name == "file2.txt");
        }

        // Cleanup
        File.Delete(file1Path);
        File.Delete(file2Path);
        File.Delete(zipPath);
        Directory.Delete(tempDir);
    }

    [Fact]
    public async Task CompressFiles_FromStreams_ShouldCreateZipWithEntries()
    {
        // Arrange
        var zipService = new ZipCompressionService();
        var zipPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");

        string file1Name = "file1.txt";
        string file2Name = "file2.txt";
        string file1Content = "Stream content for file 1";
        string file2Content = "Stream content for file 2";

        var originStreams = new (string filename, Stream stream)[]
        {
            (file1Name, new MemoryStream(System.Text.Encoding.UTF8.GetBytes(file1Content))),
            (file2Name, new MemoryStream(System.Text.Encoding.UTF8.GetBytes(file2Content)))
        };

        // Act
        await zipService.CompressFiles(originStreams, zipPath);

        // Assert
        Assert.True(File.Exists(zipPath));

        using (var zip = ZipFile.OpenRead(zipPath))
        {
            Assert.Equal(2, zip.Entries.Count);

            string entry1Text, entry2Text;
            using (var reader = new StreamReader(zip.GetEntry(file1Name)!.Open()))
                entry1Text = await reader.ReadToEndAsync();

            using (var reader = new StreamReader(zip.GetEntry(file2Name)!.Open()))
                entry2Text = await reader.ReadToEndAsync();

            Assert.Equal(file1Content, entry1Text);
            Assert.Equal(file2Content, entry2Text);
        }

        // Cleanup
        File.Delete(zipPath);
    }

    [Fact]
    public void CompressFiles_WithNoFiles_DoesNotCreateZip()
    {
        // Arrange
        var zipService = new ZipCompressionService();
        string zipPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");

        // Act
        zipService.CompressFiles(Array.Empty<string>(), zipPath);

        // Assert
        Assert.False(File.Exists(zipPath));
    }

    [Fact]
    public async Task CompressFiles_WithNoStreams_DoesNotCreateZip()
    {
        // Arrange
        var zipService = new ZipCompressionService();
        string zipPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".zip");

        // Act
        await zipService.CompressFiles(Array.Empty<(string, Stream)>(), zipPath);

        // Assert
        Assert.False(File.Exists(zipPath));
    }
}