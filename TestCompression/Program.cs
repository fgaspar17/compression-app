using CompressionApp.Application;

string sourceFile = "example.txt";
string compressedFile = "example.txt.gz";

// 1. Create a sample file with content
File.WriteAllText(sourceFile, "This is a sample text file to be compressed.");

// 2. Open the file as a stream
using FileStream sourceStream = File.OpenRead(sourceFile);

// 3. Call the Compress method
GzipCompressionService compressor = new();
await compressor.CompressFileAsync(sourceStream, compressedFile);

Console.WriteLine($"Compressed '{sourceFile}' to '{compressedFile}'.");