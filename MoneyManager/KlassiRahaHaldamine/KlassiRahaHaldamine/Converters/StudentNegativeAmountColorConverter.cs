using System.Globalization;

namespace KlassiRahaHaldamine.Converters
{
    public class StudentNegativeAmountColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal amount && amount <= 0)
            {
                return Colors.OrangeRed;
            }
            return null; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
