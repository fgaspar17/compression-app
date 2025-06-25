using CompressionApp.UILibrary.Services;
using CompressionApp.Domain.Services;
using CompressionApp.Domain.Streams;

namespace CompressionApp.Services;

public class FilesCompressService : IFileCompressService
{
    private readonly IFileSaverService _fileSaver;

    public FilesCompressService(IFileSaverService fileSaver)
    {
        _fileSaver = fileSaver;
    }

    public async Task<string> CompressFiles(IEnumerable<(string filename, Stream stream)> filesPicked, CancellationToken ct)
    {
        if (filesPicked.Count() == 1)
        {
            var filePicked = filesPicked.First();

            var progressBar = new ProgressPopupPage();
            await Application.Current.MainPage.Navigation.PushModalAsync(progressBar);
            
            var originStream = new ReadProgressStream(filePicked.stream, progressBar.UpdateProgress);
            
            await Application.Current.MainPage.Navigation.PopModalAsync();
            
            var tempCompressPath = Path.Combine(Path.GetTempPath(), "compressed.gz");
            GzipCompressionService gzip = new();
            await gzip.CompressFileAsync(originStream, tempCompressPath, ct);

            using var tempFileStream = File.OpenRead(tempCompressPath);

            return await _fileSaver.SaveFileAsync(
                filePicked.filename + ".gz",
                directory: "",
                tempFileStream,
                ct);
        }
        else
        {
            var firstFilePicked = filesPicked.First();
            var tempCompressPath = Path.Combine(Path.GetTempPath(), "compressed.zip");
            ZipCompressionService zip = new();
            await zip.CompressFiles(
            filesPicked.ToArray(),
            tempCompressPath);

            using var tempFileStream = File.OpenRead(tempCompressPath);

            return await _fileSaver.SaveFileAsync(
                firstFilePicked.filename + ".zip",
                directory: "",
                tempFileStream,
                ct);
        }
    }
}
