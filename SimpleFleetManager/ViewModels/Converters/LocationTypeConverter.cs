using System.Globalization;
using System.Windows.Data;
namespace SimpleFleetManager.ViewModels.Converters
{
    public class LocationTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int data = (int)value;
            if (data == 1) { return "Magazine"; }
            if (data == 2) { return "Nest"; }
            if (data == 3) { return "StandbyPlace"; }
            return "Wrong type";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string data = (string)value;
            if (data == "Magazine") { return 1; }
            if (data == "Nest") { return 2; }
            if (data == "StandbyPlace") { return 3; }
            return 0;
        }
    }
}
