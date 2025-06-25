using CompressionApp.Domain.Services;
using CompressionApp.Domain.Streams;
using CompressionApp.UILibrary.Services;

namespace CompressionApp.Services;

public class FilesDecompressService : IFileDecompressService
{
    private readonly IFolderPickerService _folderPicker;

    public FilesDecompressService(IFolderPickerService folderPicker)
    {
        _folderPicker = folderPicker;
    }
    public async Task<string> DecompressFiles(IEnumerable<(string filename, Stream stream)> filesPicked, CancellationToken ct)
    {
        string destinationFolder = await _folderPicker.PickFolderAsync(ct);

        GzipDecompressionService gzipDecompressionService = new();
        ZipDecompressionService zipDecompressionService = new();

        foreach (var file in filesPicked)
        {
            if (Path.GetExtension(file.filename) == ".gz")
            {
                var progressBar = new ProgressPopupPage();
                var originStream = new ReadProgressStream(file.stream, progressBar.UpdateProgress);
                await Application.Current.MainPage.Navigation.PushModalAsync(progressBar);
                await gzipDecompressionService.DecompressFileFromStreamAsync(originStream,
                    Path.Combine(destinationFolder, file.filename.TrimEnd('.', 'g', 'z')),
                    ct);
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
            else if (Path.GetExtension(file.filename) == ".zip")
            {
                await zipDecompressionService.DecompressFilesAsync(file.stream, destinationFolder
                , ct);
            }
        }

        return destinationFolder;
    }
}
