using _0.Framework.Application;

namespace Lampshade.WebApp
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file, string path)
        {
            if (file == null)
            {
                return "";
            }
            
            var savePath = $"{_webHostEnvironment.WebRootPath}//pictures//{path}";

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            var fileName = $"{DateTime.Now.ToFileTime()}-{file.FileName}";

            var fullPath = $"{savePath}//{fileName}";

            using var stream = File.Create(fullPath);

            file.CopyTo(stream);

            return fileName;
        }
    }
}
