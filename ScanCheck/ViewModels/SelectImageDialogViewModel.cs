using Caliburn.Micro;
using Microsoft.WindowsAPICodePack.Dialogs;
using ScanCheck.Core;
using ScanCheck.Entities;
using System.IO;

namespace ScanCheck.ViewModels
{
    public class SelectImageDialogViewModel : Screen
    {
        public ImageFile? Image { get; set; }
        private string? _infoText;

        public string? InfoText
        {
            get { return _infoText; }
            set
            {
                _infoText = value;
                NotifyOfPropertyChange();
            }
        }


        public SelectImageDialogViewModel(ImageFile? leftImage)
        {
            Image = leftImage;
        }

        public async void Save()
        {
            if (Image == null || string.IsNullOrWhiteSpace(Image.Path) || !File.Exists(Image.Path))
            {
                InfoText = "No valid image file selected to save.";
                return;
            }

            var dialog = new CommonSaveFileDialog
            {
                Title = "Save Image Copy As",
                DefaultFileName = Image.Name,
                DefaultExtension = Image.Extension,
                Filters = { Constants.ImageFileFilter }
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var destinationPath = dialog.FileName;

                try
                {
                    File.Copy(Image.Path, destinationPath!, overwrite: true);
                    InfoText = "Image copied successfully.";
                }
                catch (Exception ex)
                {
                    InfoText = "Failed to save image: {ex.Message}";
                }
            }

            await TryCloseAsync(true);
        }

        public void Cancel()
        {
            TryCloseAsync(false);
        }
    }
}
