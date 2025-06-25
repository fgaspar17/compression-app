namespace CompressionApp.Domain.Tests.Services;

public class GzipDecompressionServiceTests
{
    [Fact]
    public async Task DecompressFileAsync_ShouldDecompressCorrectly()
    {
        // Arrange
        var compressionService = new GzipCompressionService();
        var decompressionService = new GzipDecompressionService();
        var ct = CancellationToken.None;

        var originalText = "This is a test string to compress and decompress.";
        var tempInputPath = Path.GetTempFileName();
        var compressedPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".gz");
        var decompressedPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");

        await File.WriteAllTextAsync(tempInputPath, originalText, ct);

        // Compress first
        await compressionService.CompressFileAsync(tempInputPath, compressedPath, ct);

        // Act - Decompress
        await decompressionService.DecompressFileAsync(compressedPath, decompressedPath, ct);
        var decompressedText = await File.ReadAllTextAsync(decompressedPath, ct);

        // Assert
        Assert.Equal(originalText, decompressedText);

        // Clean up
        File.Delete(tempInputPath);
        File.Delete(compressedPath);
        File.Delete(decompressedPath);
    }

    [Fact]
    public async Task DecompressFileFromStreamAsync_ShouldDecompressCorrectly()
    {
        // Arrange
        var compressionService = new GzipCompressionService();
        var decompressionService = new GzipDecompressionService();
        var ct = CancellationToken.None;

        var originalText = "Stream test for decompression.";
        var tempCompressedPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".gz");
        var decompressedPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");

        await using var inputStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(originalText));
        await compressionService.CompressFileAsync(inputStream, tempCompressedPath, ct);

        // Act - Decompress from stream
        await using var compressedStream = File.OpenRead(tempCompressedPath);
        await decompressionService.DecompressFileFromStreamAsync(compressedStream, decompressedPath, ct);
        var decompressedText = await File.ReadAllTextAsync(decompressedPath, ct);

        // Assert
        Assert.Equal(originalText, decompressedText);

        // Clean up
        File.Delete(tempCompressedPath);
        File.Delete(decompressedPath);
    }
}
