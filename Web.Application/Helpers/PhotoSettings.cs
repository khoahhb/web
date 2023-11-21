using Microsoft.AspNetCore.Http;

namespace Web.Application.Helpers
{
    public class PhotoSettings
    {
        public long MaxBytes { get; set; }
        public string[] AllowedFileTypes { get; set; }

        public PhotoSettings()
        {
            MaxBytes = 20048000;
            AllowedFileTypes = new string[] { ".jpg", ".jpeg", ".png", ".webp" };
        }

        public bool IsSupported(string fileName)
        {
            var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
            return AllowedFileTypes.Contains(fileExtension);
        }

        public static async void SaveAvatarToUploads(IFormFile file)
        {

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }
    }

}
