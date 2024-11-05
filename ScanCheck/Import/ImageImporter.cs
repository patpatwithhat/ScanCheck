using ScanCheck.Entities;
using System.IO;

namespace ScanCheck.Import
{
    public class ImageImporter
    {
        private readonly HashSet<string> _allowedExtensions = new()
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff"
        };

        public List<ImageFile> LoadImages(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
                return new List<ImageFile>();

            return Directory.GetFiles(folderPath)
                .Where(file => _allowedExtensions.Contains(Path.GetExtension(file).ToLower()))
                .Select(file => new ImageFile
                {
                    Path = file,
                    Name = Path.GetFileNameWithoutExtension(file),
                    Extension = Path.GetExtension(file).ToLower(),
                })
                .ToList();
        }
    }
}
