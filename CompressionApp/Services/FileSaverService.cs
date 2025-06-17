using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using CompressionApp.UILibrary.Services;

namespace CompressionApp.Services;

internal class FileSaverService : IFileSaverService
{
    public async Task<string?> SaveFileAsync(string filename, string directory, Stream inputStream, CancellationToken ct)
    {

        var fileSaverResult = await FileSaver.Default.SaveAsync(filename, inputStream, ct);

        return fileSaverResult.FilePath;
    }
}