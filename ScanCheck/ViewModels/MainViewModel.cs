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

        private int _leftImageIndex;
        private int _rightImageIndex;

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
            SelectedFolderPath = "C:\\Users\\vicky\\source\\repos\\ScanCheck\\ScanCheck\\Assets";
            LoadImages();
            SetInitialImages();
#endif
        }

        private void SetInitialImages()
        {
            if (Images == null || Images.Count == 0)
                return;
            if (Images.Count >= 1)
            {
                _leftImageIndex = 0;
                LeftImage = Images[_leftImageIndex];
                LeftImage.IsSelected = true;
            }
            if (Images.Count >= 2)
            {
                _rightImageIndex = 1;
                RightImage = Images[_rightImageIndex];
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
            }
        }

        public void LeftImageSelected()
        {
            //left image stays
            //right image gets set to the next image
            RightImage.IsSelected = false;
            RightImage = Images[_rightImageIndex++];
            RightImage.IsSelected = true;
        }

        public void RightImageSelected()
        {
            //left images gets overwritten with current right
            //right image gets set to the next image
            LeftImage.IsSelected = false;
            LeftImage = RightImage;
            RightImage = Images[_rightImageIndex++];
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
        #endregion
    }
}
