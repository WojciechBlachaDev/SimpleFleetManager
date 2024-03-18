using System.Globalization;
using System.Windows.Data;

namespace SimpleFleetManager.ViewModels.Converters
{
    public class TaskTypeFromIntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string returnedString = "";
            int givenValue = (int)value;
            if (givenValue == 0)
            {
                returnedString = string.Empty;
            }
            if (givenValue == 1)
            {
                returnedString = "Standby";
            }
            if (givenValue == 2)
            {
                returnedString = "Navigation ride";
            }
            if (givenValue == 3)
            {
                returnedString = "Charging";
            }
            if (givenValue == 4)
            {
                returnedString = "Loading in magazine";
            }
            if (givenValue == 5)
            {
                returnedString = "Unloading in magazine";
            }
            if (givenValue == 6)
            {
                returnedString = "Loading in destination point";
            }
            if (givenValue == 7)
            {
                returnedString = "Unloading in destination point";
            }
            return returnedString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
