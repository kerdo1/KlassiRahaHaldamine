using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace KlassiRahaHaldamine.Converters
{
    public class EventDateToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime eventDate)
            {
                return eventDate < DateTime.Now ? Colors.Gray : Colors.Black;
            }
            return Colors.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
