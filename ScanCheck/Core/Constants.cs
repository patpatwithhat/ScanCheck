using Microsoft.WindowsAPICodePack.Dialogs;

namespace ScanCheck.Core
{
    public class Constants
    {
        public static HashSet<string> AllowedImageFileExtensions = new()
        {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff"
        };

        public static CommonFileDialogFilter ImageFileFilter = new("Image Files", "*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.tiff");
    }
}
