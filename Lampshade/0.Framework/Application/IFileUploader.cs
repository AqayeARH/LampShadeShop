using Microsoft.AspNetCore.Http;

namespace _0.Framework.Application
{
    public interface IFileUploader
    {
        string Upload(IFormFile file , string path);
    }
}
