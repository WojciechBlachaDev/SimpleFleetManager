using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace SimpleFleetManager.ViewModels.Converters
{
    public class MenuExpanderIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return new StaticResourceExtension("MenuExpanderCloseIcon");
            }
            return new StaticResourceExtension("MenuExpanderOpenIcon");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
