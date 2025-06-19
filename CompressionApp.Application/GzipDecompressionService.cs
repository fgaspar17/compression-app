using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompressionApp.Domain;

public class GzipDecompressionService
{
    public async Task DecompressFileAsync(string filePathOrigin, string filePathDestination, CancellationToken ct)
    {
        await using (var fileStreamOrigin = File.OpenRead(filePathOrigin))
        await using (var fileStreamDestination = File.OpenWrite(filePathDestination))
        await using (var gzipStream = new GZipStream(fileStreamOrigin, CompressionMode.Decompress))
        {
            await gzipStream.CopyToAsync(fileStreamDestination, ct);
        }
    }
}