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

        private int _imageIndex = 0;

        private ImageFile? _leftImage;

        public ImageFile? LeftImage
        {
            get { return _leftImage; }
            set
            {
                _leftImage = value;
                NotifyOfPropertyChange();
            }
        }

        private ImageFile? _rightImage;

        public ImageFile? RightImage
        {
            get { return _rightImage; }
            set
            {
                _rightImage = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion

        public MainViewModel(ImageImporter imageImporter)
        {
            _imageImporter = imageImporter;
#if DEBUG
            SelectedFolderPath = "C:\\Users\\kuest\\source\\repos\\ScanCheck\\ScanCheck\\Assets";
            LoadImages();
            SetInitialImages();
#endif
        }

        private void SetInitialImages()
        {
            if (Images == null || Images.Count == 0)
                return;
            _imageIndex = 0;
            if (Images.Count >= 1)
            {
                LeftImage = Images[_imageIndex];
                LeftImage.IsSelected = true;
            }
            if (Images.Count >= 2)
            {
                RightImage = Images[++_imageIndex];
                RightImage.IsSelected = true;
            }
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
            if (RightImage == null || Images == null)
                return;

            _imageIndex++;
            if (IsLastImage())
            {
                ShowSelectedImageDialog();
                return;
            }

            RightImage.IsSelected = false;
            RightImage = Images[_imageIndex];
            RightImage.IsSelected = true;
        }

        private void ShowSelectedImageDialog()
        {
            // Show dialog with the last selected image and ask if the user wants to save it
            var dialog = new SelectImageDialogViewModel(LeftImage);
            var windowManager = new WindowManager();
            windowManager.ShowDialogAsync(dialog);

        }

        public void RightImageSelected()
        {
            if (LeftImage == null || Images == null)
                return;

            _imageIndex++;
            if (IsLastImage())
            {
                ShowSelectedImageDialog();
                return;
            }

            LeftImage.IsSelected = false;
            LeftImage = RightImage;
            RightImage = Images[_imageIndex];
            RightImage.IsSelected = true;
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

        private bool IsLastImage()
        {
            return Images?.Count <= _imageIndex;
        }
        #endregion
    }
}
