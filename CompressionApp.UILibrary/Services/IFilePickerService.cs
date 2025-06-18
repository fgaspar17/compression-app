namespace CompressionApp.UILibrary.Services;

public interface IFilePickerService
{
    Task<string?> PickFileAsync(string title, CancellationToken ct);
    Task<IEnumerable<string>> PickMultipleFilesAsync(string title, CancellationToken ct);
}