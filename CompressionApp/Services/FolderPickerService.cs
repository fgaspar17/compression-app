using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using CompressionApp.UILibrary.Services;

namespace CompressionApp.Services;

public class FolderPickerService : IFolderPickerService
{
    public async Task<string> PickFolderAsync(CancellationToken cancellationToken)
    {
        var result = await FolderPicker.Default.PickAsync(cancellationToken);
        return result.IsSuccessful ? result.Folder.Path : throw new InvalidOperationException();
    }
}
