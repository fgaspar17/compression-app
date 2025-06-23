using CompressionApp.Domain;
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
                await gzipDecompressionService.DecompressFileFromStreamAsync(file.stream,
                    Path.Combine(destinationFolder, file.filename.TrimEnd('.', 'g', 'z')),
                    ct);
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
