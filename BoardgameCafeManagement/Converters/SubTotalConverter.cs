using System.Globalization;
using System.Windows.Data;

namespace BoardgameCafeManagement.Converters
{
    public class SubTotalConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 2 && values[0] is int price && values[1] is int quantity)
            {
                return (price * quantity * 1000).ToString("N0", new CultureInfo("es-ES")) + "đ";
            }
            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
