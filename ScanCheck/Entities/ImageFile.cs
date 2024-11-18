using Caliburn.Micro;

namespace ScanCheck.Entities
{
    public class ImageFile : PropertyChangedBase
    {
        public string? Path { get; set; }
        public string? Name { get; set; }
        public string? Extension { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        private SelectionStates _selectionState;

        public SelectionStates SelectionState
        {
            get { return _selectionState; }
            set
            {
                _selectionState = value;
                NotifyOfPropertyChange();
            }
        }

        public ImageFile()
        {
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ImageFile other)
                return false;

            return string.Equals(Path, other.Path, StringComparison.OrdinalIgnoreCase) &&
                   string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Path?.ToLowerInvariant(), Name?.ToLowerInvariant());
        }

        #region inner structures
        public enum SelectionStates
        {
            NotSelected,
            IsSelected,
            IsDisplayed,
            WasDisplayed
        }
        #endregion
    }
}
