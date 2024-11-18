using ScanCheck.Entities;
using System.Globalization;
using System.Windows.Data;

namespace ScanCheck.Converters
{
    class SelectionStateToWidthHeightConverter : IValueConverter
    {
        private const int DefaultWidthHeight = 75;
        private const int DisplayedWidthHeight = 90;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (!(value is ImageFile.SelectionStates selectionState))
                return DefaultWidthHeight;

            if (selectionState == ImageFile.SelectionStates.IsDisplayed)
                return DisplayedWidthHeight;
            return DefaultWidthHeight;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
