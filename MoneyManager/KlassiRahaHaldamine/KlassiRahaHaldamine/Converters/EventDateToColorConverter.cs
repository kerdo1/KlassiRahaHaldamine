using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace KlassiRahaHaldamine.Converters
{
    public class EventDateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime eventDate && eventDate < DateTime.Now)
            {
                return Colors.Gray;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
