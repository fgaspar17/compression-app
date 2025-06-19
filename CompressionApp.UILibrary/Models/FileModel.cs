namespace CompressionApp.UILibrary.Models;

internal class FileModel
{
    public IEnumerable<string> SourceFilesPath { get; set; }
    public string DestinationFilePath { get; set; }
}