using Caliburn.Micro;
using Microsoft.WindowsAPICodePack.Dialogs;
using ScanCheck.Core;
using ScanCheck.Entities;
using ScanCheck.Import;
using System.IO;

namespace ScanCheck.ViewModels
{
    public class MainViewModel : Screen
    {
        private readonly ImageImporter _imageImporter;
        private int _imageIndex = 0;

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

        private ImageFile? _leftImage;

        public ImageFile? LeftImage
        {
            get { return _leftImage; }
            set
            {
                if (_leftImage != null)
                    _leftImage.SelectionState = ImageFile.SelectionStates.WasDisplayed;

                _leftImage = value;

                if (_leftImage != null)
                    _leftImage.SelectionState = ImageFile.SelectionStates.IsSelected;

                NotifyOfPropertyChange();
            }
        }

        private ImageFile? _rightImage;

        public ImageFile? RightImage
        {
            get { return _rightImage; }
            set
            {
                if (value != null && value.Equals(_leftImage))
                    return;

                if (_rightImage != null)
                    _rightImage.SelectionState = ImageFile.SelectionStates.WasDisplayed;

                _rightImage = value;

                if (_rightImage != null)
                    _rightImage.SelectionState = ImageFile.SelectionStates.IsDisplayed;

                UpdateImageIndex(_rightImage);

                NotifyOfPropertyChange();
            }
        }

        private string _infoText = string.Empty;

        public string InfoText
        {
            get { return _infoText; }
            set { _infoText = value; NotifyOfPropertyChange(); }
        }

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
                SetInitialImages();
            }
        }

        public void LeftImageSelected()
        {
            if (RightImage == null || Images == null || IsLastImage())
                return;

            _imageIndex++;

            var image = Images[_imageIndex];
            if (image.Equals(LeftImage))
                LeftImageSelected();
            else
                RightImage = image;
        }

        public void RightImageSelected()
        {
            if (LeftImage == null || Images == null || IsLastImage())
                return;

            _imageIndex++;

            var tempImage = RightImage;
            RightImage = Images[_imageIndex];
            LeftImage = tempImage;
        }

        public async void SaveImage()
        {
            if (LeftImage == null || string.IsNullOrWhiteSpace(LeftImage.Path) || !File.Exists(LeftImage.Path))
            {
                InfoText = "No valid image file selected to save.";
                return;
            }

            var destinationPath = OpenSaveFileDialog();
            if (string.IsNullOrWhiteSpace(destinationPath))
            {
                InfoText = "Failed to select destination path.";
                return;
            }

            try
            {
                File.Copy(LeftImage.Path, destinationPath!, overwrite: true);
                InfoText = "Image copied successfully.";
            }
            catch (Exception ex)
            {
                InfoText = $"Failed to save image: {ex.Message}";
            }

            await TryCloseAsync(true);
        }
        #endregion

        #region Helpers
        private void SetInitialImages()
        {
            if (Images == null || Images.Count == 0)
                return;

            _imageIndex = 0;
            if (Images.Count >= 1)
                LeftImage = Images[_imageIndex];

            if (Images.Count >= 2)
                RightImage = Images[++_imageIndex];
        }

        private void LoadImages()
        {
            if (SelectedFolderPath != null)
                Images = _imageImporter.LoadImages(SelectedFolderPath);
        }

        private string OpenSaveFileDialog()
        {
            var dialog = new CommonSaveFileDialog
            {
                Title = "Save Image Copy As",
                DefaultFileName = LeftImage?.Name,
                DefaultExtension = LeftImage?.Extension,
                Filters = { Constants.ImageFileFilter }
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                return dialog.FileName ?? string.Empty;

            return string.Empty;
        }

        private bool IsLastImage()
        {
            return Images?.Count <= _imageIndex;
        }

        private void UpdateImageIndex(ImageFile? rightImage)
        {
            if (rightImage == null)
                return;
            var newIndex = Images?.IndexOf(rightImage);
            _imageIndex = newIndex ?? _imageIndex;
        }
        #endregion
    }
}
