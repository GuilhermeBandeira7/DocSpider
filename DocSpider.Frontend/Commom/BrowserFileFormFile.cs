using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace DocSpider.Frontend.Commom;

public class BrowserFileFormFile : IFormFile
{
    private readonly IBrowserFile _browserFile;
    private Stream _stream;

    public BrowserFileFormFile(IBrowserFile browserFile)
    {
        _browserFile = browserFile;
        _stream = browserFile.OpenReadStream();
    }

    public string ContentType => _browserFile.ContentType;
    public string ContentDisposition => $"form-data; name=\"file\"; filename=\"{FileName}\"";
    public IHeaderDictionary Headers => new HeaderDictionary();
    public long Length => _browserFile.Size;
    public string Name => "file";
    public string FileName => _browserFile.Name;

    public void CopyTo(Stream target)
    {
        _stream.CopyTo(target);
    }

    public async Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
    {
        await _stream.CopyToAsync(target, cancellationToken);
    }

    public Stream OpenReadStream()
    {
        return _stream;
    }

    public void Dispose()
    {
        _stream?.Dispose();
    }
}
