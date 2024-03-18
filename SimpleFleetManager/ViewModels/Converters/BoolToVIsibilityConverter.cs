using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SimpleFleetManager.ViewModels.Converters
{
    public class BoolToVIsibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isVisible = (bool)value;

            if (isVisible)
            {
                return Visibility.Visible;
            }
            else { return Visibility.Collapsed; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility isVisible = (Visibility)value;
            return Visibility.Visible == isVisible ? true : (object)false;
        }
    }
}
