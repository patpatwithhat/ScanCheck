using ScanCheck.Entities;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ScanCheck.Converters
{
    class SelectionStatesToVisabilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ImageFile.SelectionStates selectionState))
                return Visibility.Visible;
            if (selectionState == ImageFile.SelectionStates.IsSelected)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
