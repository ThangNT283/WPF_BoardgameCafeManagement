using System.Globalization;
using System.Windows.Data;

namespace BoardgameCafeManagement.Converters
{
    public class StatusToComboBoxConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool status && parameter is string param)
            {
                switch (param)
                {
                    case "ActiveInactive":
                        return status ? "Active" : "Inactive";
                    case "BlankInUsed":
                        return status ? "In Used" : "Blank";
                    case "AvailableInUsed":
                        return status ? "Available" : "In Used";
                    default:
                        return ""; // Default value for unknown parameter
                }
            }
            return ""; // Default value
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string statusString && parameter is string param)
            {
                switch (param)
                {
                    case "ActiveInactive":
                        return statusString == "Active";
                    case "BlankInUsed":
                        return statusString == "In Used";
                    case "AvailableInUsed":
                        return statusString == "Available";
                    default:
                        return false; // Default value for unknown parameter
                }
            }
            return false; // Default value
        }
    }
}
