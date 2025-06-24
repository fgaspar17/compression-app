using System.IO.Compression;

namespace CompressionApp.Domain.Tests;

public class GzipCompressionServiceTests
{
    [Fact]
    public async Task CompressFileAsync_FromFilePath_CreatesCompressedFile()
    {
        // Arrange
        var service = new GzipCompressionService();
        var ct = CancellationToken.None;

        var tempInput = Path.GetTempFileName();
        var tempOutput = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".gz");
        await File.WriteAllTextAsync(tempInput, "Test content for compression", ct);

        // Act
        await service.CompressFileAsync(tempInput, tempOutput, ct);

        // Assert
        Assert.True(File.Exists(tempOutput));
        Assert.True(new FileInfo(tempOutput).Length > 0);

        // Clean up
        File.Delete(tempInput);
        File.Delete(tempOutput);
    }

    [Fact]
    public async Task CompressFileAsync_FromStream_CreatesCompressedFile()
    {
        // Arrange
        var service = new GzipCompressionService();
        var ct = CancellationToken.None;
        var tempOutput = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".gz");

        var testData = "Stream content to compress";
        await using var originStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(testData));

        // Act
        await service.CompressFileAsync(originStream, tempOutput, ct);

        // Assert
        Assert.True(File.Exists(tempOutput));
        Assert.True(new FileInfo(tempOutput).Length > 0);

        // Clean up
        File.Delete(tempOutput);
    }

    [Fact]
    public async Task CompressedFile_CanBeDecompressedToOriginalContent()
    {
        // Arrange
        var service = new GzipCompressionService();
        var ct = CancellationToken.None;

        var originalText = "This is the original content.";
        var tempInput = Path.GetTempFileName();
        var tempCompressed = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".gz");

        await File.WriteAllTextAsync(tempInput, originalText, ct);

        // Compress
        await service.CompressFileAsync(tempInput, tempCompressed, ct);

        // Decompress to memory
        await using (var compressedStream = File.OpenRead(tempCompressed))
        {
            await using var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress);
            using var reader = new StreamReader(gzipStream);
            var decompressedText = await reader.ReadToEndAsync();
            
            // Assert
            Assert.Equal(originalText, decompressedText);
        }

        // Clean up
        File.Delete(tempInput);
        File.Delete(tempCompressed);
    }
}
