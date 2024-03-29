using System.Globalization;
using System.Windows.Data;
namespace SimpleFleetManager.ViewModels.Converters
{
    public class BoolToStatusGreyRedFlipped : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isOk = (bool)value;
            if (isOk) { return "/Models/Resources/Icons/StatusError.png"; } else { return "/Models/Resources/Icons/StatusUnactive.png"; }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
