using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;



namespace Infrastructure.Utility;

public class FileUtility
{
    private readonly IWebHostEnvironment env;
    private readonly IConfiguration configuration;

    public FileUtility(IWebHostEnvironment env,IConfiguration configuration)
    {
        this.env = env;
        this.configuration = configuration;
    }

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

    public string SaveFileInFolder(IFormFile file, string entityName)
    {
        var appExecutionRootPath = env.WebRootPath;
        var mediaRootPath = configuration.GetValue<string>("MediaPath");
        var folderRootPath = entityName;
        CheckAndCreateDirectory(appExecutionRootPath, mediaRootPath, folderRootPath);

        var newFileName = $"{DateTime.Now.Ticks.ToString()}{GetFileExtension(file.FileName)}";

        var newFilePath = Path.Combine(appExecutionRootPath, mediaRootPath, folderRootPath, newFileName);
        var byteArray = ConvertToByteArray(file);

        using var writer = new BinaryWriter(File.OpenWrite(newFilePath));
        writer.Write(byteArray);
        return newFileName;

    }


    private void CheckAndCreateDirectory(string appRootPath , string mediaRootPath , string entityFolderName)
    {
        var mediaFullPath = Path.Combine(appRootPath, mediaRootPath);
        if (!Directory.Exists(mediaFullPath))
        {
            Directory.CreateDirectory(mediaFullPath);
        }
        var entityFolderFullPath = Path.Combine(mediaFullPath, entityFolderName);
        if (!Directory.Exists(entityFolderFullPath))
        {
            Directory.CreateDirectory(entityFolderFullPath);
        }
    }
}
