using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utility;

public class FileUtility
{
    public byte[] ConvertToByteArray(IFormFile file)
    {
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        return ms.ToArray();
    }

    public string GetFileExtension(string fileName)
    {
        var fileInfo = new FileInfo(fileName);
        return fileInfo.Extension;
    }
}
