namespace CompressionApp.UILibrary.Models;

internal class FileModel
{
    public IEnumerable<Stream> SourceStreams { get; set; }
    public IEnumerable<string> SourceFilesPath { get; set; }
    public string DestinationFilePath { get; set; }
}