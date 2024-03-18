using System.Globalization;
using System.Windows.Data;

namespace SimpleFleetManager.ViewModels.Converters
{
    public class BoolToStatusGreyYellow : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isOk = (bool)value;
            string result;
            if (!isOk)
            {
                result = "/Models/Resources/Icons/WarningYellow.png";
            }
            else
            {
                result = "/Models/Resources/Icons/StatusUnactive.png";
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
