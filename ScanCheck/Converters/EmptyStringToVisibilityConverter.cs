using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ScanCheck.Converters
{
    public class EmptyStringToVisibilityConverter : IValueConverter
    {
        public bool Invert { get; set; }

        public bool UseHidden { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isEmpty = string.IsNullOrWhiteSpace(value as string);

            if (Invert)
                isEmpty = !isEmpty;

            return isEmpty ? (UseHidden ? Visibility.Hidden : Visibility.Collapsed) : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Two-way binding is not supported for EmptyStringToVisibilityConverter.");
        }
    }
}
