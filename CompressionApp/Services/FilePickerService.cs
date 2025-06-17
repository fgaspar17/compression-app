using CompressionApp.UILibrary.Services;

namespace CompressionApp.Services;

internal class FilePickerService : IFilePickerService
{
    public async Task<string?> PickFileAsync(CancellationToken ct)
    {
        var file = await FilePicker.PickAsync();
        return file?.FullPath;
    }
}