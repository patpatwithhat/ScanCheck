using ScanCheck.Entities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScanCheck.Controls
{
    /// <summary>
    /// Interaction logic for ScrollableImageGallery.xaml
    /// </summary>
    public partial class ScrollableImageGallery : UserControl
    {
        public ScrollableImageGallery()
        {
            InitializeComponent();
        }

        #region Dependency Properties
        public static readonly DependencyProperty ImagesProperty =
            DependencyProperty.Register("Images", typeof(IEnumerable<ImageFile>), typeof(ScrollableImageGallery),
                new PropertyMetadata(null));

        public IEnumerable<ImageFile> Images
        {
            get => (IEnumerable<ImageFile>)GetValue(ImagesProperty);
            set => SetValue(ImagesProperty, value);
        }

        public IEnumerable<ImageFile>? DisplayedImages
        {
            get => Images?.Where(i => i.SelectionState != ImageFile.SelectionStates.NotSelected);
        }

        public static readonly DependencyProperty SelectedImageProperty =
            DependencyProperty.Register("SelectedImage", typeof(ImageFile), typeof(ScrollableImageGallery),
                new PropertyMetadata(null));

        public ImageFile SelectedImage
        {
            get => (ImageFile)GetValue(SelectedImageProperty);
            set => SetValue(SelectedImageProperty, value);
        }
        #endregion

        #region Event Handlers

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is ImageFile selectedImage)
            {
                SelectedImage = selectedImage;
            }
        }
        #endregion
    }
}
