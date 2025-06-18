using CompressionApp.UILibrary.Services;

namespace CompressionApp.Services;

internal class FilePickerService : IFilePickerService
{
    public async Task<string?> PickFileAsync(string title, CancellationToken ct)
    {
        var file = await FilePicker.PickAsync(
            new PickOptions
            {
                PickerTitle = title,
            });
        return file?.FullPath;
    }

    public async Task<IEnumerable<string>> PickMultipleFilesAsync(string title, CancellationToken ct)
    {
        var files = await FilePicker.PickMultipleAsync(
            new PickOptions
            {
                PickerTitle = title
            });
        return files.Select(f => f.FullPath);
    }
}