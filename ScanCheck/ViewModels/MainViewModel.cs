using Caliburn.Micro;
using Microsoft.WindowsAPICodePack.Dialogs;
using ScanCheck.Entities;
using ScanCheck.Import;

namespace ScanCheck.ViewModels
{
    public class MainViewModel : Screen
    {
        private readonly ImageImporter _imageImporter;
        private int _carouselStartIndex;

        #region Properties
        private string? _selectedFolderPath;

        public string? SelectedFolderPath
        {
            get => _selectedFolderPath;
            set
            {
                _selectedFolderPath = value;
                NotifyOfPropertyChange();
            }
        }

        private List<ImageFile>? _images;
        public List<ImageFile>? Images
        {
            get => _images;
            private set
            {
                _images = value;
                NotifyOfPropertyChange();
                NotifyOfPropertyChange(() => LeftImage);
                NotifyOfPropertyChange(() => RightImage);
            }
        }


        public ImageFile? LeftImage => Images?.Count > 0 ? Images[0] : null;
        public ImageFile? RightImage => Images?.Count > 1 ? Images[1] : null;

        #endregion

        public MainViewModel(ImageImporter imageImporter)
        {
            _imageImporter = imageImporter;
        }

        #region Button Actions
        public void BrowseFolder()
        {
            var dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,
                Title = "Select a Folder"
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SelectedFolderPath = dialog.FileName;
                LoadImages();
            }
        }

        public void LeftImageSelected()
        {
            LeftImage.IsSelected = !LeftImage.IsSelected;
        }

        public void RightImageSelected()
        {
            RightImage.IsSelected = !RightImage.IsSelected;

        }

        #endregion

        #region Helpers
        private void LoadImages()
        {
            if (SelectedFolderPath != null)
            {
                Images = _imageImporter.LoadImages(SelectedFolderPath);
            }
        }
        #endregion
    }
}
