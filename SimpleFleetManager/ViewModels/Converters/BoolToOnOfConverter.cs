using System.Globalization;
using System.Windows.Data;

namespace SimpleFleetManager.ViewModels.Converters
{
    public class BoolToOnOfConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                return "ON";
            }
            else
            {
                return "OFF";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
