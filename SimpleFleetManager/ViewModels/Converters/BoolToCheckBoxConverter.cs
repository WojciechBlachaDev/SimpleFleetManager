using System.Globalization;
using System.Windows.Data;

namespace SimpleFleetManager.ViewModels.Converters
{
    public class BoolToCheckBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isChecked = (bool)value;
            if (isChecked)
            {
                return checked((bool)value);
            }
            else
            {
                return unchecked((bool)value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
