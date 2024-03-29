using System.Globalization;
using System.Windows.Data;
namespace SimpleFleetManager.ViewModels.Converters
{
    public class LogLevelStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnedString = "";
            int givenValue = (int)value;
            if (givenValue == 1) { returnedString = "DEBUG"; }
            if (givenValue == 2) { returnedString = "INFO"; }
            if (givenValue == 3) { returnedString = "WARNING"; }
            if (givenValue == 4) { returnedString = "ERROR"; }
            if (givenValue == 5) { returnedString = "CRITICAL"; }
            return returnedString;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int returnedInt = 6;
            string givenString = (string)value;
            if (givenString == "DEBUG") { returnedInt = 1; }
            if (givenString == "INFO") { returnedInt = 2; }
            if (givenString == "WARNING") { returnedInt = 3; }
            if (givenString == "ERROR") { returnedInt = 4; }
            if (givenString == "CRITICAL") { returnedInt = 5; }
            return returnedInt;
        }
    }
}
