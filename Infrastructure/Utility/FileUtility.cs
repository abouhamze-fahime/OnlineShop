using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

using System.Security.Cryptography;



namespace Infrastructure.Utility;

public class FileUtility
{
    private readonly IWebHostEnvironment env;
    private readonly IConfiguration configuration;
    private readonly IHttpContextAccessor httpContextAccessor;

    public FileUtility(
        IWebHostEnvironment env,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor
        
        )
    {
        this.env = env;
        this.configuration = configuration;
        this.httpContextAccessor = httpContextAccessor;
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

    public string GetFileFullPath (string fileName , string entityName)
    {
        var appRootPath = env.WebRootPath;
        var mediaRootPath = configuration.GetValue<string>("MediaPath");
        return Path.Combine(appRootPath , mediaRootPath, entityName, fileName);
    }

    public string SaveFileInFolder(IFormFile file, string entityName , bool isEncrypt=false)
    {
        var appRootPath = env.WebRootPath;
        var mediaRootPath = configuration.GetValue<string>("MediaPath");
        
        CheckAndCreateDirectory(appRootPath, mediaRootPath, entityName);

        var newFileName = $"{DateTime.Now.Ticks.ToString()}{GetFileExtension(file.FileName)}";

        var newFilePath = Path.Combine(appRootPath, mediaRootPath, entityName, newFileName);
        var byteArray = ConvertToByteArray(file);
        if (isEncrypt)
        {
            byteArray = EncryptFile(byteArray);
        }
       

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

    public string ConvertToBase64(byte[] data)
    {
        return Convert.ToBase64String(data);
    }

    public string GetFileUrl(string thumbnailFileName, string entityName)
    {
        var host = httpContextAccessor.HttpContext.Request.Host.Value;
        var isHttps=httpContextAccessor.HttpContext.Request.IsHttps;
        var folderPath = GetEntityFolderUrl(host, entityName , isHttps);
        var httpMode = isHttps ? "https" : "http";
        return $"{httpMode}://{folderPath}/{thumbnailFileName}";
    }

    private string GetEntityFolderUrl(string host,string entityName , bool isHttps)
    {
        var mediaRootPath = configuration.GetValue<string>("MediaPath").Replace("\\", "/");
        return $"{host}/{mediaRootPath}/{entityName}";
    }

    public string GetEncryptedFileActionUrl(string thumbnailFileName, string entityName)
    {
        var host = httpContextAccessor.HttpContext.Request.Host.Value;
        var isHttps = httpContextAccessor.HttpContext.Request.IsHttps;
        var httpMode = isHttps? "https" : "http";
        return $"{httpMode}://{host}/Media/{entityName}/{thumbnailFileName}";
    }

    public byte[] EncryptFile(byte[] fileContent)
    {
        string EncryptionKey = configuration.GetValue<string>("FileEncryptionKey");
        using(Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV =  pdb.GetBytes(16);
            using (MemoryStream ms = new ())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(fileContent, 0, fileContent.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                return ms.ToArray();
            }
        }
    }


    public byte[] Decrypt(byte[] fileContent)
    {
        string EncryptionKey = configuration.GetValue<string>("FileEncryptionKey");
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new ())
            {
                using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(fileContent, 0, fileContent.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                return ms.ToArray();
            }
        }
    }
}
