using ControlzEx.Theming;
using ScanCheck.Entities;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ScanCheck.Converters
{
    public class SelectionStatesToBrushConverter : IValueConverter
    {
        public Brush NotSelectedBrush { get; set; } = Brushes.Purple;
        public Brush WasDisplayedBrush { get; set; } = Brushes.Black;
        public Brush IsDisplayedBrush { get; set; } = GetAccentBrush();

        private static Brush GetAccentBrush()
        {
            var theme = ThemeManager.Current.DetectTheme(Application.Current);

            if (theme != null)
            {
                return new SolidColorBrush(theme.PrimaryAccentColor) ?? Brushes.LightGreen;
            }

            return Brushes.LightGreen;
        }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ImageFile.SelectionStates selectionState))
                return NotSelectedBrush;

            switch (selectionState)
            {
                case ImageFile.SelectionStates.NotSelected:
                    return NotSelectedBrush;
                case ImageFile.SelectionStates.IsDisplayed:
                    return IsDisplayedBrush;
                case ImageFile.SelectionStates.WasDisplayed:
                    return WasDisplayedBrush;
                default:
                    return NotSelectedBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
