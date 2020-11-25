using System;
using System.Globalization;
using Xamarin.Forms;

namespace BorderCrossing.Converters
{
    public class DateConverter2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            if (!(value is DateTime d))
            {
                return value;
            }

            var universalTime = d.ToUniversalTime();

            var shortDate = universalTime.ToShortDateString();
            var shortTime = universalTime.ToString("hh:mm");

            return $"{shortDate} {shortTime}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
