using System.Globalization;
using System.Windows.Data;
namespace SimpleFleetManager.ViewModels.Converters
{
    public class JobStepTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int data = (int)value;
            if (data == 1) { return "Point to point ride"; }
            if (data == 2) { return "Charging"; }
            if (data == 3) { return "Get palette from magazine"; }
            if (data == 4) { return "Drop palette in magazine"; }
            if (data == 5) { return "Get palette from nest"; }
            if (data == 6) { return "Leave palette at nest"; }
            return "Wrong task type";
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string data = (string)value;
            if (data == "Point to point ride") { return 1; }
            if (data == "Charging") { return 2; }
            if (data == "Get palette from magazine") { return 3; }
            if (data == "Drop palette in magazine") { return 4; }
            if (data == "Get palette from nest") { return 5; }
            if (data == "Leave palette at nest") { return 6; }
            return -1;
        }
    }
}
