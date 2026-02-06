using AuthServiceIN6BM.Application.Interfaces;

namespace AuthServiceIN6BM.Api.Models;

public class FormFileAdapter : IFileData
{
    private readonly IFormFile _formfile;
    private byte[]? _data;

    public FormFileAdapter(IFormFile formFile)
    {
        ArgumentNullException.ThrowIfNull(formFile);
        _formfile = formFile;
    }

    public byte[] Data
    {
        get
        {
            if (_data == null)
            {
                using var memoryStream = new MemoryStream();
                _formfile.CopyTo(memoryStream);
                _data = memoryStream.ToArray();
            }
            return _data;
        }
    }

    public string ContentType => _formfile.ContentType;
    public string FileName => _formfile.FileName;
    public long Size => _formfile.Length;
}