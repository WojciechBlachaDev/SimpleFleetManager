using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SimpleFleetManager.ViewModels.Converters
{
    public class EmptyStringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? temp = value as string;
            if (temp == "" || string.IsNullOrEmpty(temp)) return Visibility.Collapsed;
            else return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
