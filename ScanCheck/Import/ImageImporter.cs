using ScanCheck.Core;
using ScanCheck.Entities;
using System.IO;

namespace ScanCheck.Import
{
    public class ImageImporter
    {


        public List<ImageFile> LoadImages(string folderPath)
        {
            if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
                return new List<ImageFile>();

            var fileExtensions = Constants.AllowedImageFileExtensions.ToList();

            return Directory.GetFiles(folderPath)
                .Where(file => fileExtensions.Contains(Path.GetExtension(file).ToLower()))
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
