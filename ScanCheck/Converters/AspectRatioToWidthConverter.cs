using System.Globalization;
using System.Windows.Data;

namespace ScanCheck.Converters
{
    public class AspectRatioToWidthConverter : IMultiValueConverter
    {
        private const double DefaultWidth = 100.0;
        private const double Padding = 100;

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 &&
                values[0] is double originalHeight &&
                values[1] is double originalWidth &&
                values[2] is double targetHeight &&
                originalHeight > 0)
            {
                double aspectRatio = originalWidth / originalHeight;
                double calculatedWidth = targetHeight * aspectRatio - Padding;

                return Math.Max(calculatedWidth, DefaultWidth);
            }

            return DefaultWidth; // Fallback for invalid inputs
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("AspectRatioToWidthConverter only supports one-way conversion.");
        }
    }
}
