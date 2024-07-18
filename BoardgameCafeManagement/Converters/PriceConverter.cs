using System.Globalization;
using System.Windows.Data;

namespace BoardgameCafeManagement.Converters
{
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int price)
            {
                return (price * 1000).ToString("N0", new CultureInfo("es-ES")) + "đ";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double price)
            {
                return price / 1000;
            }
            return value;
        }
    }
}
