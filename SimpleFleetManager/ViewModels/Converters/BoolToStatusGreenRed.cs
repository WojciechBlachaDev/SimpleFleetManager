using System.Globalization;
using System.Windows.Data;
namespace SimpleFleetManager.ViewModels.Converters
{
    public class BoolToStatusGreenRed : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isOk = (bool)value;
            if (isOk) { return "/Models/Resources/Icons/StatusOk.png"; } else { return "/Models/Resources/Icons/StatusError.png"; }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
