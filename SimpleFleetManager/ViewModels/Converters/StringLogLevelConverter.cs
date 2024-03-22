using System.Globalization;
using System.Windows.Data;

namespace SimpleFleetManager.ViewModels.Converters
{
    public class StringLogLevelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int returnedInt = 1;
            string givenString = (string)value;
            if (givenString == "DEBUG")
            {
                returnedInt = 1;
            }
            if (givenString == "INFO")
            {
                returnedInt = 2;
            }
            if (givenString == "WARNING")
            {
                returnedInt = 3;
            }
            if (givenString == "ERROR")
            {
                returnedInt = 4;
            }
            if (givenString == "FATAL")
            {
                returnedInt = 5;
            }
            return returnedInt;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
