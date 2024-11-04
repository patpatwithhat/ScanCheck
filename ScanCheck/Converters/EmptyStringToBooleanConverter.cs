using System.Globalization;
using System.Windows.Data;

namespace ScanCheck.Converters
{

    public class EmptyStringToBooleanConverter : IValueConverter
    {
        public bool Invert { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
                bool isEmpty = string.IsNullOrWhiteSpace(str);
                return Invert ? !isEmpty : isEmpty;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Two-way binding not supported for EmptyStringToBooleanConverter.");
        }
    }

}
