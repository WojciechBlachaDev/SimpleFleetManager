using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SimpleFleetManager.ViewModels.Converters
{
    public class MenuExpanderIconConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return Application.Current.Resources["MenuExpanderCloseIcon"];
            }
            return Application.Current.Resources["MenuExpanderOpenIcon"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
