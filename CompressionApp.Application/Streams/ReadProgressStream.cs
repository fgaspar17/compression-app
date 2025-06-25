namespace CompressionApp.Domain.Streams;

public class ReadProgressStream : Stream
{
    private readonly Stream _innerStream;
    private readonly Action<double> _progressCallback;
    private long _totalBytesRead = 0;

    public ReadProgressStream(Stream innerStream, Action<double> progressCallback)
    {
        _innerStream = innerStream;
        _progressCallback = progressCallback;
    }

    public override bool CanRead => _innerStream.CanRead;

    public override bool CanSeek => _innerStream.CanSeek;

    public override bool CanWrite => false;

    public override long Length => _innerStream.Length;

    public override long Position { get => _innerStream.Position; set => _innerStream.Position = value; }

    public override void Flush()
    {
        _innerStream.Flush();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        int bytesRead = _innerStream.Read(buffer, offset, count);
        TrackProgress(bytesRead);
        return bytesRead;
    }

    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        int bytesRead = await _innerStream.ReadAsync(buffer, offset, count, cancellationToken);
        TrackProgress(bytesRead);
        return bytesRead;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        return _innerStream.Seek(offset, origin);
    }

    public override void SetLength(long value)
    {
        _innerStream.SetLength(value);
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new InvalidOperationException("This stream does not support writing.");
    }

    private void TrackProgress(int bytesRead)
    {
        _totalBytesRead += bytesRead;
        double result = (double)_totalBytesRead / (double)_innerStream.Length;
        _progressCallback(result);
    }
}