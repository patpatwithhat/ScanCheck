using ScanCheck.Entities;
using System.Windows;
using System.Windows.Controls;

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

        public static readonly DependencyProperty ImagesProperty =
            DependencyProperty.Register("Images", typeof(IEnumerable<ImageFile>), typeof(ScrollableImageGallery),
                new PropertyMetadata(null));

        public IEnumerable<ImageFile> Images
        {
            get => (IEnumerable<ImageFile>)GetValue(ImagesProperty);
            set => SetValue(ImagesProperty, value);
        }

    }
}
