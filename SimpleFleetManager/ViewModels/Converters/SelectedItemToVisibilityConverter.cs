using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SimpleFleetManager.ViewModels.Converters
{
    public class SelectedItemToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int isVisible = (int)value;
            if (isVisible > 0)
            {
                return Visibility.Visible;
            }
            else { return Visibility.Collapsed; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
