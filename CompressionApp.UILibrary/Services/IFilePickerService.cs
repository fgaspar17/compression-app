namespace CompressionApp.UILibrary.Services;

public interface IFilePickerService
{
    Task<string?> PickFileAsync(CancellationToken ct);
}