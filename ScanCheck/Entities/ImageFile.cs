using Caliburn.Micro;

namespace ScanCheck.Entities
{
    public class ImageFile : PropertyChangedBase
    {
        public string? Path { get; set; }
        public string? Name { get; set; }
        public string? Extension { get; set; }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        public ImageFile()
        {

        }
    }
}
