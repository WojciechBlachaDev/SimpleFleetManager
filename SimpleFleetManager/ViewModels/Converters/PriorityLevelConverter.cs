using System.Globalization;
using System.Windows.Data;

namespace SimpleFleetManager.ViewModels.Converters
{
    public class PriorityLevelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int data = (int)value;
            if (data == 1) { return "Very Low"; }
            if (data == 2) { return "Low"; }
            if (data == 3) { return "Normal"; }
            if (data == 4) { return "High"; }
            if (data == 5) { return "Very High"; }
            return "No priority data";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string data = (string)value;
            if (data == "Very Low") { return 1; }
            if (data == "Low") { return 2; }
            if (data == "Normal") { return 3; }
            if (data == "High") { return 4; }
            if (data == "Very High") { return 5; }
            return -1;
        }
    }
}
