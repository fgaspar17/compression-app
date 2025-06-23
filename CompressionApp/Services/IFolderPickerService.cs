namespace CompressionApp.UILibrary.Services;

public interface IFolderPickerService
{
    Task<string> PickFolderAsync(CancellationToken cancellationToken);
}