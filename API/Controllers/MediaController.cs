using Infrastructure.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MediaController : ControllerBase
    {
        private readonly FileUtility file;

        public MediaController(FileUtility file )
        {
            this.file = file;
        }
        [HttpGet("/Media/{entity}/{fileName}")]
        public async Task<IActionResult> Get( string entity , string fileName)
        {
            var filePath = file.GetFileFullPath(fileName , entity);
            byte[] encryptedData = await System.IO.File.ReadAllBytesAsync(filePath);
            var decryptedData = file.Decrypt(encryptedData);
            return new FileContentResult(decryptedData , "application/txt");
        }
    }
}
