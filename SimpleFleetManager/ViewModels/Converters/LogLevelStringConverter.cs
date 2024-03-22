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
            if (givenValue == 1)
            {
                returnedString = "DEBUG";
            }
            if (givenValue == 2)
            {
                returnedString = "INFO";
            }
            if (givenValue == 3)
            {
                returnedString = "WARNING";
            }
            if (givenValue == 4)
            {
                returnedString = "ERROR";
            }
            if (givenValue == 5)
            {
                returnedString = "FATAL";
            }
            return returnedString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
